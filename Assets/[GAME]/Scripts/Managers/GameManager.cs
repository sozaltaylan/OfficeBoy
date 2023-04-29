using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeFever.Exceptions;
using OfficeFever.Managers;

namespace OfficeFever.Managers 
{
    public class GameManager : MonoSingleton<GameManager>
    {
        #region Variables
        [SerializeField] private float totalMoney;
        [SerializeField] private float upgradePrinterCost;
        [SerializeField] private float upgradeWorkerCost;
        [SerializeField] private float upgradeHireCost;
        [SerializeField] private float upgradeHireSpeedCost;

        private int upgradePrinterLevel = 1;
        private int upgradeWorkerLevel = 1;
        private int upgradeHireLevel = 1;
        private int upgradeHireSpeedLevel = 1;

        #region Properties
        public float TotalMoney => totalMoney;
        public float UpgradePrinterCost => upgradePrinterCost;
        public int UpgradePrinterLevel => upgradePrinterLevel;

        public float UpgradeWorkerCost => upgradeWorkerCost;
        public int UpgradeWorkerLevel => upgradeWorkerLevel;

        public float UpgradeHireCost => upgradeHireCost;
        public int UpgradeHireLevel => upgradeHireLevel;

        public float UpgradeHireSpeedCost => upgradeHireSpeedCost;
        public int UpgradeHireSpeedLevel => upgradeHireSpeedLevel;

        #endregion
        #endregion

        #region Event
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void OnDisable()
        {
            UnsubcribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.onAddMoney += SetTotalMoney;
        }
        private void UnsubcribeEvents()
        {
            CoreGameSignals.onAddMoney -= SetTotalMoney;
        }
        #endregion

        #region Methods
        public void SetTotalMoney(float amount)
        {
            totalMoney += amount;

            if (totalMoney < 0)
            {
                totalMoney = 0;
            }
        }

        public bool IsUpgradePrinter()
        {
            bool isEnough = (UpgradePrinterCost <= TotalMoney) ? true : false;
            return isEnough;
        }
        public bool IsUpgradeWorker()
        {
            bool isEnough = (UpgradeWorkerCost <= TotalMoney) ? true : false;
            return isEnough;
        }
        public bool IsUpgradeHire()
        {

            bool isEnough = (upgradeHireCost <= totalMoney) ? true : false;
            return isEnough;
        }
        public bool IsUpgradeHireSpeed()
        {
            bool isEnough = (upgradeHireSpeedCost <= TotalMoney) ? true : false;
            return isEnough;
        }

        public void SetPrintUpgrade()
        {
            if (IsUpgradePrinter())
            {
                CoreGameSignals.onAddMoney(-upgradePrinterCost);
                upgradePrinterLevel++;
                upgradePrinterCost *= 2;
                UIManager.Instance.SetPrinterUITexts();

            }
        }

        public void SetWorkerUpgrade()
        {
            if (IsUpgradeWorker())
            {
                CoreGameSignals.onAddMoney(-upgradeWorkerCost);
                upgradeWorkerLevel++;
                upgradeWorkerCost *= 2;
                UIManager.Instance.SetWorkerUITexts();
            }
        }
        public void SetHireUpgrade()
        {
            Debug.Log("game maanger 1");
            if (IsUpgradeHire())
            {
                Debug.Log("game maanger 2");
                HireManager.Instance.CreateHireWorker();
                CoreGameSignals.onAddMoney.Invoke(-upgradeHireCost);
                upgradeHireLevel++;
                upgradeHireCost *= 2;
                UIManager.Instance.SetHireUITexts();
            }
        }
        public void SetHireSpeedUpgrade()
        {
            if (IsUpgradeHireSpeed())
            {
                CoreGameSignals.onAddMoney(-upgradeHireSpeedCost);
                upgradeHireSpeedLevel++;
                upgradeHireSpeedCost *= 2;
                UIManager.Instance.SetHireSpeedUITexts();
            }
        }



        #endregion
    }
}

