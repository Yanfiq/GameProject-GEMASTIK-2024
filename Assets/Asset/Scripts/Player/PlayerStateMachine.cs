using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    CharacterController _controller;
    PlayerInput _playerInput;
    Animator _animator;

    //Optimize animation calling
    int _isWalkingHash;
    int _isRunningHash;
    int _isJumpingHash;
    int _isFallingHash;
    int _JumpCountHash;

    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    Vector3 _appliedMovement;
    Vector3 _cameraRelativeMovement;
    bool _isMovementPressed;
    bool _isRunPressed;

    float _rotationFactorPerFrame = 15f;
    float _runMultiplier = 4f;

    float _gravity = -9.8f;

    public LayerMask _groundMask;
    public Transform _groundChecker;
    public float _groundDistance = 1f;

    bool _isJumpPressed = false;
    float _initialJumpVelocity;
    float _maxJumpHeight = 2f;
    float _maxJumpTime = .75f;
    bool _isJumping = false;
    bool _requireNewJumpPress = false;
    int _jumpCount = 0;
    Dictionary<int, float> _initialJumpVelocities = new Dictionary<int, float>();
    Dictionary<int, float> _jumpGravities = new Dictionary<int, float>();
    Coroutine _currentJumpRoutine = null;

    PlayerBaseState _currentState; //The current state of player
    PlayerStateFactory _states;

    [SerializeField] float interactRange = 1f;
    [SerializeField] LayerMask interactableLayerMask;
    [SerializeField] Interactable currentInteractable;
    //[SerializeField] List<Interactable> interactables = new List<Interactable>();

    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public CharacterController CharacterController { get { return _controller; } }
    public Animator Animator { get { return _animator; } }
    public Coroutine CurrentJumpRoutine { get { return _currentJumpRoutine; } set { _currentJumpRoutine = value; } }
    public Dictionary<int, float> InitialJumpVelocities { get { return _initialJumpVelocities; } }
    public Dictionary<int, float> JumpGravities { get { return _jumpGravities; } }
    public int JumpCount { get { return _jumpCount;  } set { _jumpCount = value; } }
    public int IsJumpingHash { get { return _isJumpingHash; } }
    public int IsWalkingHash { get { return _isWalkingHash; } }
    public int IsRunningHash { get { return _isRunningHash; } }
    public int IsFallingHash { get { return _isFallingHash; } }
    public int JumpCountHash { get { return _JumpCountHash; } }
    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public bool IsRunPressed { get { return _isRunPressed; } }
    public bool RequireNewJumpPress { get { return _requireNewJumpPress; } set { _requireNewJumpPress = value; } }
    public bool IsJumping { set { _isJumping = value; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public float Gravity { get { return _gravity; } }
    public float CurrentMovementX { get { return _currentMovement.x; } set { _currentMovement.x = value; } }
    public float CurrentMovementY { get { return _currentMovement.y; } set { _currentMovement.y = value; } }
    public float CurrentMovementZ { get { return _currentMovement.z; } set { _currentMovement.z = value; } }
    public float AppliedMovementX { get { return _appliedMovement.x; } set { _appliedMovement.x = value; } }
    public float AppliedMovementY { get { return _appliedMovement.y; } set { _appliedMovement.y = value; } }
    public float AppliedMovementZ { get { return _appliedMovement.z; } set { _appliedMovement.z = value; } }
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } }
    public float RunMultiplier { get { return _runMultiplier; } }

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _playerInput = new PlayerInput();
        _animator = GetComponent<Animator>();

        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _isFallingHash = Animator.StringToHash("isFalling");
        _JumpCountHash = Animator.StringToHash("JumpCount");

        //Walk
        _playerInput.CharacterControls.Move.started += OnMovementInput;   // Started / Button Down
        _playerInput.CharacterControls.Move.canceled += OnMovementInput;  // End / Button Up
        _playerInput.CharacterControls.Move.performed += OnMovementInput; // Update / Hold Button

        //Run
        _playerInput.CharacterControls.Run.started += OnRun;
        _playerInput.CharacterControls.Run.canceled += OnRun;

        //Jump
        _playerInput.CharacterControls.Jump.started += OnJump;
        _playerInput.CharacterControls.Jump.canceled += OnJump;

        //Interact
        _playerInput.CharacterControls.Interact.started += OnInteract;

        SetupJumpVariables();
    }

    void SetupJumpVariables()
    {
        float timeToApex = _maxJumpTime / 2;
        float initialgravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
        float secondJumpGravity = (-2 * (_maxJumpHeight * 2)) / Mathf.Pow((timeToApex * 1.25f), 2);
        float secondInitialJumpVelocity = (2 * (_maxJumpHeight * 2)) / (timeToApex * 1.25f);
        float thirdJumpGravity = (-2 * (_maxJumpHeight * 4)) / Mathf.Pow((timeToApex * 1.5f), 2);
        float thirdInitialJumpVelocity = (2 * (_maxJumpHeight * 4)) / (timeToApex * 1.5f);

        _initialJumpVelocities.Add(1, _initialJumpVelocity);
        _initialJumpVelocities.Add(2, secondInitialJumpVelocity);
        _initialJumpVelocities.Add(3, thirdInitialJumpVelocity);

        _jumpGravities.Add(0, initialgravity);
        _jumpGravities.Add(1, initialgravity);
        _jumpGravities.Add(2, secondJumpGravity);
        _jumpGravities.Add(3, thirdJumpGravity);
    }

    void Start()
    {
        _controller.Move(_appliedMovement * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        _currentState.UpdateStates();

        _cameraRelativeMovement = ConvertToCameraSpace(_appliedMovement);
        _controller.Move(_cameraRelativeMovement * Time.deltaTime);

        CheckInteractable();
    }

    void OnCollisionEnter(Collision collision)
    {
        _currentState.OnCollisionEnter(collision);
    }

    // Execute OnInteract from current interactable
    void OnInteract(InputAction.CallbackContext context)
    {
        currentInteractable.OnInteract();
    }

    // Check for interactable NPC/enviroment or pickable item
    void CheckInteractable()
    {
        //float fovAngle = 60;

        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange, interactableLayerMask);

        if(colliders.Length > 0)
        {
            currentInteractable = colliders[0].GetComponent<Interactable>();
            /*foreach(Collider c in colliders)
            {
                Vector3 dirToTarget = (c.transform.position - transform.position).normalized;

                float angleToTarget = Vector3.Angle(transform.forward, dirToTarget);

                if(angleToTarget < (fovAngle / 2))
                {

                    //interactables.Add(c.GetComponent<Interactable>());
                }
            }*/
        }

        //currentInteractable = ClosestInteractable(interactables);
    }

    Interactable ClosestInteractable(List<Interactable> inters)
    {
        float lowestDistance = float.MaxValue;

        Interactable closest = null;

        for (int i = 0; i < inters.Count; i++)
        {
            Vector3 directionToTarget = inters[i].transform.position - transform.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < lowestDistance) continue;
            lowestDistance = dSqrToTarget;
            closest = inters[i];
        }

        return closest;
    }

    void HandleRotation()
    {
        Vector3 _positionToLookAt;
        // The next position
        _positionToLookAt.x = _cameraRelativeMovement.x;
        _positionToLookAt.y = 0.0f;
        _positionToLookAt.z = _cameraRelativeMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (_isMovementPressed)
        {
            Quaternion _targetRotation = Quaternion.LookRotation(_positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, _targetRotation, _rotationFactorPerFrame * Time.deltaTime);
        }
    }

    Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        float currentYValue = vectorToRotate.y;

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0.0f;
        cameraRight.y = 0.0f;

        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        Vector3 cameraForwardZProduct = vectorToRotate.z * cameraForward;
        Vector3 cameraRightZProduct = vectorToRotate.x * cameraRight;

        Vector3 vectorRotatedToCameraSpace = cameraForwardZProduct + cameraRightZProduct;
        vectorRotatedToCameraSpace.y = currentYValue;
        return vectorRotatedToCameraSpace;
    }

    //It's User defined Function, not generated !!!
    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }

    //It's User defined Function, not generated !!!
    private void OnRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    //It's User defined Function, not generated !!!
    private void OnJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
    }

    void OnEnable()
    {
        //Enabling Character Controls
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        //Enabling Character Controls
        _playerInput.CharacterControls.Disable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
