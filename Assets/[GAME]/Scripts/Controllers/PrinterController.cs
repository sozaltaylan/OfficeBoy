using DG.Tweening;
using OfficeFever.Managers;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

 namespace OfficeFever.Controllers
{
    public class PrinterController : MonoBehaviour
    {
        #region Variables

        [SerializeField] private PaperController paperPrefab;

        [SerializeField] private List<PaperController> papersOnTable = new List<PaperController>();

        [SerializeField] private Transform paperCreatePos;
        [SerializeField] private Transform paperStackPos;

        [SerializeField] private float multplierX;
        [SerializeField] private float multplierY;
        [SerializeField] private float multplierZ;
        [SerializeField] private float createTime;

        private float _time;

        [SerializeField] private int MaxPaper;
        [SerializeField] private int increaseMaxPaper;

        public Transform agentDestinationPos;



        #endregion
        private void Update()
        {
            if (papersOnTable.Count >= MaxPaper) return;

            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                PrintPaper();
                _time = createTime;
            }

        }

        private void PrintPaper()
        {
            var paperObj = Instantiate(paperPrefab.gameObject, paperCreatePos);
            var paperController = paperObj.GetComponent<PaperController>();
            paperController.MoveTarget(paperStackPos.transform.position + GetPaperPosition(), this.transform);
            AddList(paperController);
        }
        private Vector3 GetPaperPosition()
        {
            float x = 0;
            float y = 0;
            float z = 0;

            Vector3 pos = Vector3.zero;

            for (int i = 0; i < papersOnTable.Count; i++)
            {
                x = papersOnTable.Count % 4;
                var valueZ = papersOnTable.Count % 8;
                z = valueZ / 4;
                var valueY = papersOnTable.Count / 8;
                y = Mathf.FloorToInt(valueY);


            }

            x *= multplierX;
            y *= multplierY;
            z *= multplierZ;

            pos = new Vector3(x, y, z);
            return pos;

        }


        private PaperController GetFirstPaper()
        {
            if (papersOnTable.Count == 0) return default;

            PaperController paper = default;

            foreach (var item in papersOnTable)
            {
                paper = item;
                break;
            }

            return paper;
        }
        private void OnTriggerStay(Collider collision)
        {

            if (collision.gameObject.TryGetComponent(out StackController stack))
            {
                var rb = stack.transform.gameObject.GetComponent<Rigidbody>();
                if (rb.IsSleeping() || papersOnTable.Count == 0) return;

                var paper = GetFirstPaper();
                paper.MoveTargetPlayer(stack.StackPos.transform, stack.GetPaperStackPos()); 
                stack.AddList(paper);
                RemoveList(paper);
            }

        }
        private void RemoveList(PaperController paper)
        {
            if (papersOnTable.Contains(paper))
            {
                papersOnTable.Remove(paper);
            }
        }

        private void AddList(PaperController paper)
        {
            if (!papersOnTable.Contains(paper))
            {
                papersOnTable.Add(paper);
            }
        }

     
        public void SetMaxPaper()
        {
            if (!GameManager.Instance.IsUpgradePrinter()) return;

            MaxPaper += increaseMaxPaper;
        }
    }
}

