using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeFever.Exceptions;
using OfficeFever.Managers;

namespace OfficeFever.Managers
{
    public class HireManager : MonoSingleton<HireManager>
    {
        #region Variables

        [SerializeField] private GameObject hireWorkerPrefab;
        [SerializeField] private float hireWorkerSpeed;
        [SerializeField] private float upgradeSpeedValue;

        #region Properties
        public float HireWorkerSpeed => hireWorkerSpeed;
        #endregion
        #endregion

        #region Methods

        public void CreateHireWorker()
        {
           // if (!GameManager.Instance.IsUpgradeHire()) return;

            Instantiate(hireWorkerPrefab, Vector3.zero, Quaternion.identity);
        }

        public void UpgradeHireWorkerSpeed()
        {
            if (!GameManager.Instance.IsUpgradeHireSpeed()) return;
            hireWorkerSpeed += upgradeSpeedValue;
        }
        
        #endregion

    }

}

