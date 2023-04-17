using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Climber : MonoBehaviour
{
    public static event Action ClimbActive, ClimbInActive;

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private InputActionProperty _velocityRight, _velocityLeft;

    [SerializeField] private GameObject _leftController, _rightController;

    private bool _rightActive, _leftActive;

    private void Start()
    {
        ClimbInteractable.ClimbHandActivated += HandActivated;
        ClimbInteractable.ClimbHandDeactivated += HandDeactivated;
    }

    private void OnDestroy()
    {
        ClimbInteractable.ClimbHandActivated -= HandActivated;
        ClimbInteractable.ClimbHandDeactivated -= HandDeactivated;
    }

    private void HandActivated(string _controllerName)
    {
        if (_controllerName == _leftController.name)
        {
            _leftActive = true;
            _rightActive = false;
        }
        else
        {
            _leftActive = false;
            _rightActive = true;
        }

        ClimbActive?.Invoke();
    }

    private void HandDeactivated(string _controllerName)
    {
        if (_rightActive && _controllerName == _rightController.name)
        {
            _rightActive = false;
            ClimbInActive?.Invoke();
        }
        else if (_leftActive && _controllerName == _leftController.name)
        {
            _leftActive = false;
            ClimbInActive?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (_leftActive || _rightActive) 
        {
            Climb();
        }
    }

    private void Climb()
    {
        Vector3 velocity = _leftActive ? _velocityLeft.action.ReadValue<Vector3>() : _velocityRight.action.ReadValue<Vector3>();
        _characterController.Move(_characterController.transform.rotation * -velocity * Time.fixedDeltaTime);
    }
}