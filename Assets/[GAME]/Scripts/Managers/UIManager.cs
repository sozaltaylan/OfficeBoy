using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using OfficeFever.Exceptions;
using DG.Tweening;

namespace OfficeFever.Managers
{
    public class UIManager : MonoSingleton<UIManager>
    {
        #region Variables

        public TextMeshProUGUI totalMoneyText;

        [Header("Printer Text")]
        [SerializeField] private TextMeshProUGUI upgradePrinterCostText;
        [SerializeField] private TextMeshProUGUI upgradePrinterLevelText;

        [Header("Worker Text")]
        [SerializeField] private TextMeshProUGUI upgradeWorkerCostText;
        [SerializeField] private TextMeshProUGUI upgradeWorkerLevelText;

        [Header("Hire Text")]
        [SerializeField] private TextMeshProUGUI upgradeHireCostText;
        [SerializeField] private TextMeshProUGUI upgradeHireLevelText;

        [Header("HireSpeed Text")]
        [SerializeField] private TextMeshProUGUI upgradeHireSpeedCostText;
        [SerializeField] private TextMeshProUGUI upgradeHireSpeedLevelText;

        [Header("Panels")]
        [SerializeField] private GameObject managerPanel;
        [SerializeField] private GameObject hrPanel;

        [Header("Canvas")]
        [SerializeField] private Canvas joystickCanvas;

        private Vector3 currentScale;
        #endregion

        #region Methods

        #region Event
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CoreGameSignals.onAddMoney += UpdateTotalMoney;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.onAddMoney -= UpdateTotalMoney;
        }
        #endregion
        private void Start()
        {

            HandleStart();
        }

        private void HandleStart()
        {
            totalMoneyText.text = "$" + GameManager.Instance.TotalMoney;
            upgradePrinterCostText.text = "$" + GameManager.Instance.UpgradePrinterCost;
            upgradeWorkerCostText.text = "$" + GameManager.Instance.UpgradeWorkerCost;
            upgradeHireCostText.text = "$" + GameManager.Instance.UpgradeHireCost;
            upgradeHireSpeedCostText.text = "$" + GameManager.Instance.UpgradeHireSpeedCost;
            currentScale = totalMoneyText.transform.localScale;

        }
        public void UpdateTotalMoney(float mny)
        {
            var totalAmount = GameManager.Instance.TotalMoney;
            var value = totalAmount < 1000 ? totalAmount.ToString() : string.Format("{0:#.#}k", totalAmount / 1000f);
            totalMoneyText.text = "$" + value;
            totalMoneyText.transform.DOShakeScale(.1f, .1f, 1, 1f).OnComplete(() => totalMoneyText.transform.localScale = currentScale);
            totalMoneyText.DOKill(true);
        }

        public void SetManagerPanel(bool active)
        {
            managerPanel.gameObject.SetActive(active);
        }
        public void SetHrPanel(bool active)
        {
            hrPanel.gameObject.SetActive(active);
        }

        public void SetPrinterUITexts()
        {
            upgradePrinterLevelText.text = "LEVEL " + GameManager.Instance.UpgradePrinterLevel;

            var cost = GameManager.Instance.UpgradePrinterCost;
            var value = cost < 1000 ? cost.ToString() : string.Format("{0:#.#}k", cost / 1000f);
            upgradePrinterCostText.text = "$" + value;
        }

        public void SetWorkerUITexts()
        {
            upgradeWorkerLevelText.text = "LEVEL " + GameManager.Instance.UpgradeWorkerLevel;

            var cost = GameManager.Instance.UpgradeWorkerCost;
            var value = cost < 1000 ? cost.ToString() : string.Format("{0:#.#}k", cost / 1000f);
            upgradeWorkerCostText.text = "$" + value;
        }

        public void SetHireUITexts()
        {
            upgradeHireLevelText.text = "LEVEL " + GameManager.Instance.UpgradeHireLevel;

            var cost = GameManager.Instance.UpgradeHireCost;
            var value = cost < 1000 ? cost.ToString() : string.Format("{0:#.#}k", cost / 1000f);
            upgradeHireCostText.text = "$" + value;
        }

        public void SetHireSpeedUITexts()
        {
            upgradeHireSpeedLevelText.text = "LEVEL " + GameManager.Instance.UpgradeHireSpeedLevel;

            var cost = GameManager.Instance.UpgradeHireSpeedCost;
            var value = cost < 1000 ? cost.ToString() : string.Format("{0:#.#}k", cost / 1000f);
            upgradeHireSpeedCostText.text = "$" + value;
        }

        #endregion
    }

}