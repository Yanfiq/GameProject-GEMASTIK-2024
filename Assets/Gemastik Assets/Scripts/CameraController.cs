using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    Camera_IA _cameraInput;
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;

    [SerializeField] float speed;

    void Awake()
    {
        _cameraInput = new Camera_IA();
        _cameraInput.Camera.Enable();

        _cameraInput.Camera.Move.started += OnMovementInput;
        _cameraInput.Camera.Move.canceled += OnMovementInput;
        _cameraInput.Camera.Move.performed += OnMovementInput;
    }

    void Update()
    {
        _currentMovement = new Vector3(_currentMovementInput.x, 0, _currentMovementInput.y);
        transform.position += _currentMovement.normalized * speed * Time.deltaTime;
        //transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
    }
}
