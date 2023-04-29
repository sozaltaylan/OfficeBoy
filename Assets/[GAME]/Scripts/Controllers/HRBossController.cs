using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeFever.Managers;
using TaylanGame.Controllers;

public class HRBossController : MonoBehaviour
{
    private InputController _inputController;

    private void Awake()
    {
        _inputController = FindObjectOfType<InputController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController player))
        {
            UIManager.Instance.SetHrPanel(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController player))
        {
            UIManager.Instance.SetHrPanel(false);
        }
    }
}
