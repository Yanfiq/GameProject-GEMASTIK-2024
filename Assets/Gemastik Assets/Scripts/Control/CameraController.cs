using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;

    [SerializeField] float speed;

    void Start()
    {
        InputActionController.Instance.InputAction.Camera.Move.started += OnMovementInput;
        InputActionController.Instance.InputAction.Camera.Move.canceled += OnMovementInput;
        InputActionController.Instance.InputAction.Camera.Move.performed += OnMovementInput;
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
