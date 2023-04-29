using OfficeFever.Managers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

namespace OfficeFever.Controllers
{
    public class HireController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private List<PrinterController> printersOnScene = new List<PrinterController>();
        [SerializeField] private List<PaperController> papers = new List<PaperController>();
        [SerializeField] private List<OfficeWorkerController> workersOnScene = new List<OfficeWorkerController>();


        [SerializeField] private NavMeshAgent agent;

        [SerializeField] private AnimationController animationController;
        [SerializeField] private StackController stackController;

        [SerializeField] private int papersMaxCount;
        #endregion

        private void Start()
        {
            agent.destination = FindNearesPrinter();
        }
        private void Update()
        {
            SetAnimation();
            SetAgentMovement();
        }

        private void SetAnimation()
        {
            var speed = agent.velocity.magnitude;
            animationController.OnWalkAnimation(speed);
        }

        private Vector3 FindNearesPrinter()
        {
            printersOnScene = PrinterManager.Instance.GetPrinters();

            Vector3 nearestPrinter = printersOnScene[0].transform.position;
            float distance = float.PositiveInfinity;

            for (int i = 0; i < printersOnScene.Count; i++)
            {
                var printer = printersOnScene[i];

                var distanceValue = Vector3.Distance(this.transform.position, printer.agentDestinationPos.transform.position);

                if (distanceValue < distance)
                {
                    distance = distanceValue;
                    nearestPrinter = printer.agentDestinationPos.transform.position;
                }
            }

            return nearestPrinter;

        }

        private Vector3 FindAvaibleWorker()
        {
            Vector3 pos = default;
            int papersOnDeskCount = int.MaxValue;
            workersOnScene = OfficeWorkerManager.Instance.GetWorkers();

            for (int i = 0; i < workersOnScene.Count; i++)
            {
                var worker = workersOnScene[i];
                var onDeskPaper = worker.GetOnDeskPapers();

                if (onDeskPaper.Count < papersOnDeskCount)
                {
                    papersOnDeskCount = onDeskPaper.Count;
                    pos = worker.agentDestinationPos.transform.position;
                }

            }

            return pos;
        }

        private void SetAgentMovement()
        {
            papers = stackController.GetPapers();
            agent.speed = HireManager.Instance.HireWorkerSpeed;

            if (papers.Count >= papersMaxCount)
            {
                agent.destination = FindAvaibleWorker();
            }
            if (papers.Count == 0)
            {
                agent.destination = FindNearesPrinter();
            }
        }

    }

}

