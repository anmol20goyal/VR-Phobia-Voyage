using System;
using UnityEngine;

public class CharacterControllerGravity : MonoBehaviour
{
    [SerializeField] private float _characterHeight, _characterRadius;
    [SerializeField] private float _characterHeight_Default, _characterRadius_Default;

    private CharacterController _characterController;
    private bool _climbing = false;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _characterHeight_Default = _characterController.height;
        _characterRadius_Default = _characterController.radius;

        Climber.ClimbActive += ClimbActive;
        Climber.ClimbInActive += ClimbInActive;
    }

    private void OnDestroy()
    {
        Climber.ClimbActive -= ClimbActive;
        Climber.ClimbInActive -= ClimbInActive;
    }

    private void FixedUpdate()
    {
        if (!_characterController.isGrounded && _climbing)
        {
            _characterController.SimpleMove(new Vector3());
        }
    }

    private void ClimbInActive()
    {
        _climbing = true;
    }

    private void ClimbActive()
    {
        _climbing = false;
    }

    public void AlterCharacterController()
    {
        _characterController.height = _characterHeight;
        _characterController.radius = _characterRadius;
    }

    public void CC_BackToNormal()
    {
        _characterController.height = _characterHeight_Default;
        _characterController.radius = _characterRadius_Default;
    }
}