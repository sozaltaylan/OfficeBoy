using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeFever.Managers;

public class OfficeManagerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController player))
        {
            UIManager.Instance.SetManagerPanel(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController player))
        {
            UIManager.Instance.SetManagerPanel(false);
        }
    }
}
