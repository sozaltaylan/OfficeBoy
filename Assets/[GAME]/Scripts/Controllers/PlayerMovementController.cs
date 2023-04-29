using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TaylanGame.Controllers;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Variables

   
    [SerializeField] private DynamicJoystick dynamicJoystick;
    [SerializeField] private AnimationController animationController;

    private Rigidbody _rigidbody;
    private PlayerData _playerData;
    private InputController inputController;
    private Vector3 _movement;
    #endregion

    #region Methods
    public void Init(Rigidbody rigidbody, PlayerData data, InputController input)
    {
        _rigidbody = rigidbody;
        _playerData = data;
        inputController = input;
    }

    public void HandleUpdate()
    {
        SetMovement();
        SetWalkAnimation();

    }
    private void SetMovement()
        {
            if (inputController.IsInput)
            {
                float moveHorizontal = dynamicJoystick.Horizontal;
                float moveVertical = dynamicJoystick.Vertical;


            _movement = new Vector3(x: moveHorizontal * _playerData.movementSpeed * Time.deltaTime, y: 0, z: moveVertical * _playerData.rotationSpeed * Time.deltaTime);
            _rigidbody.transform.position += _movement;

                Vector3 directions = Vector3.forward * moveVertical + Vector3.right * moveHorizontal;
            _rigidbody.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_movement), _playerData.rotationSpeed * Time.deltaTime);

            }
            else if (Input.GetMouseButtonUp(0))
            {
            _rigidbody.transform.rotation = Quaternion.LookRotation(transform.forward);
            }
        }

    private void SetWalkAnimation()
    {
        var speed = _movement.magnitude;

        if (inputController.IsInput)
        {
           
            animationController.OnWalkAnimation(speed);
        }
        else if (!inputController.IsInput)
        {
            speed = 0;
            animationController.OnWalkAnimation(speed);
        }
    }
    #endregion
}
