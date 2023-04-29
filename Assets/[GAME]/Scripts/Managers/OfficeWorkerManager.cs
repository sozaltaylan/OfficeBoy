using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeFever.Exceptions;


namespace OfficeFever.Managers
{
    public class OfficeWorkerManager : MonoSingleton<OfficeWorkerManager>
    {
        #region Variables

        [SerializeField] private List<OfficeWorkerController> officeWorkers = new List<OfficeWorkerController>();

        [SerializeField] private float workDelay;
        [SerializeField] private float upgradeValue;

        #region Properties 
        public float WorkDelay => workDelay;
        public float UpgradeValue => upgradeValue;
        #endregion
        #endregion

        #region Methods
        public void SetWorkFastUpgrade()
        {
            if (!GameManager.Instance.IsUpgradeWorker()) return;

            workDelay -= upgradeValue;
        }
        public void AddList(OfficeWorkerController worker)
        {
            if (!officeWorkers.Contains(worker))
            {
                officeWorkers.Add(worker);
            }
        }

        public List<OfficeWorkerController> GetWorkers()
        {
            return officeWorkers;
        }
        #endregion


    }
}
