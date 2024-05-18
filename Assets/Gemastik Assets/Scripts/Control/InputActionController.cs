using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionController : MonoBehaviour
{
    public static InputActionController Instance { get; private set; }
    Camera_IA _inputAction;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        _inputAction = new Camera_IA();
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] Mode currentMode;

    public Camera_IA InputAction { get { return _inputAction; } }

    void Start()
    {
        _inputAction.Camera.Enable();
    }

    public void ChangeMode(int newMode)
    {
        currentMode = (Mode)newMode;
        switch (currentMode)
        {
            case Mode.Build:
            case Mode.Traffic:
                _inputAction.Car.Disable();
                _inputAction.Camera.Enable();
                break;
            case Mode.Drive:
                _inputAction.Camera.Disable();
                _inputAction.Car.Enable();
                break;
        }
    }

    void Update()
    {

    }
}

public enum Mode
{
    Build,
    Drive,
    Traffic
}
