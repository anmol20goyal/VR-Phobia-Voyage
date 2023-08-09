using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using CommonUsages = UnityEngine.XR.CommonUsages;

public class Climber : MonoBehaviour
{
    public Image imageCol;
    public TMP_Text debugTxt;

    public static event Action ClimbActive, ClimbInactive;

    [SerializeField] private CharacterController _characterController;
    //[SerializeField] private ContinuousMovement _continuousMovement;
    [SerializeField] private InputActionProperty _velocityRight, _velocityLeft;
    public ActionBasedController _leftClimbingHand;
    public ActionBasedController _rightClimbingHand;

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
        //if (_controllerName == _leftController.name)
        //{
        //    _leftActive = true;
        //    _rightActive = false;
        //}
        //else
        //{
        //    _leftActive = false;
        //    _rightActive = true;
        //}

        _leftActive = _controllerName == _leftController.name;
        _rightActive = _controllerName == _rightController.name;

        ClimbActive?.Invoke();
    }

    private void HandDeactivated(string _controllerName)
    {
        if (_rightActive && _controllerName == _rightController.name)
        {
            _rightActive = false;
            ClimbInactive?.Invoke();
        }
        else if (_leftActive && _controllerName == _leftController.name)
        {
            _leftActive = false;
            ClimbInactive?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (_leftActive || _rightActive)
        {
            Climb();
            imageCol.color = Color.yellow;
        }
        else
            imageCol.color = Color.red;
    }

    private void Climb()
    {
        Vector3 velocity = _leftActive ? _velocityLeft.action.ReadValue<Vector3>() : _velocityRight.action.ReadValue<Vector3>();
        debugTxt.text = velocity.ToString();
        _characterController.Move(_characterController.transform.rotation * -velocity * Time.fixedDeltaTime);

        //InputDevices.GetDeviceAtXRNode(climbingHand.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 vel);

        //debugTxt.text = vel.ToString();
        //_characterController.Move(-velocity * Time.fixedDeltaTime);
    }
}