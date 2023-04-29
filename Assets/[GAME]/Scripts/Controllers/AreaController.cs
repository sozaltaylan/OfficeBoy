using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using OfficeFever.Managers;

namespace OfficeFever.Controllers
{
    public class AreaController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float cost;
        [SerializeField] private float firstTotalMoney;

        [SerializeField] private TextMeshProUGUI costText;

        [SerializeField] private GameObject worker;

        [SerializeField] private Transform createPos;
        [SerializeField] private Transform particleCreatePos;

        [SerializeField] private ParticleSystem particle;
        #endregion
        #region Methods
        private void Update()
        {
            if (cost <= 0)
            {
                CreateWorker();
            }
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerController player))
            {
                firstTotalMoney = GameManager.Instance.TotalMoney;
                CoreGameSignals.onAddMoney(-cost);

                var cur = 0f;
                cur = cost;
                var lastCost = cost - firstTotalMoney;
                DOVirtual.Float(cur, lastCost, 1f, DOMoney);

            }
        }
        private void DOMoney(float x)
        {
            cost = x;
            costText.text = "$" + (int)x;
        }
        private void CreateWorker()
        {
            var workerPre = Instantiate(worker, createPos.transform.position, Quaternion.identity);
            var workerScript = workerPre.gameObject.GetComponent<OfficeWorkerController>();
            if (workerScript != null) { OfficeWorkerManager.Instance.AddList(workerScript);}
            workerPre.transform.DOMoveY(-0.30f, .3f);
            var particlePre = Instantiate(particle, particleCreatePos.transform.position, Quaternion.identity);
            Destroy(particlePre, 2);

            Destroy(this.gameObject);
        }
        #endregion
    }

}
