using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaylanGame.Controllers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables


    [SerializeField] private AnimationController animationController;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerMovementController playerMovementController;
    [SerializeField] private InputController inputController;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject money;

    #endregion

    private void Start()
    {
        playerMovementController.Init(rb, playerData, inputController);
    }

    private void FixedUpdate()
    {
        playerMovementController.HandleUpdate();
    }

    



}

