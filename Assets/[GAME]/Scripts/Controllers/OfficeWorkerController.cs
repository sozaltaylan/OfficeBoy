using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using OfficeFever.Managers;

public class OfficeWorkerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private List<PaperController> onDeskPapers = new List<PaperController>();
    private List<MoneyController> moneys = new List<MoneyController>();

    private PlayerController player;
    
  

    [SerializeField] private float multiplierX;
    [SerializeField] private float multiplierZ;
    [SerializeField] private float multiplierY;
  
    private float _timer;

    [SerializeField] private GameObject slepingZZ;

    [SerializeField] private Transform paperStackPos;
    public Transform agentDestinationPos; 

    private bool _isCanCreateMoney;

    [Header("#Money Ref#")]
    [SerializeField] private Transform moneyStackPos;
    [SerializeField] private GameObject money;

    private float moneyTimer;
    [SerializeField] private float maxMoneyTime = .2f;
    #endregion
    #region Methods

    private void Start()
    {
        player = PlayerManager.Instance.GetPlayer();
    }

    private void Update()
    {

        if (player)
        {
            var target = player.transform.position;
            target.y = moneyStackPos.transform.position.y;

            var dist = (moneyStackPos.transform.position - target).magnitude;
            if (Mathf.Abs(dist) <= 1.5f)
            {
                moneyTimer -= Time.deltaTime;

                if (moneyTimer <= 0 && moneys.Count > 0)
                {
                    moneyTimer = maxMoneyTime;
                    moneys[moneys.Count - 1].DOMove(player.transform);

                }
               
            }
        }

        _timer -= Time.deltaTime;
        if (_timer <= 0 && onDeskPapers.Count > 0)
        {
            Work();
            _timer = OfficeWorkerManager.Instance.WorkDelay;
            _isCanCreateMoney = true;
        }
        if (_isCanCreateMoney == true)
        {
            StartCoroutine(CreateMoney());
            _isCanCreateMoney = false;
        }

        if (onDeskPapers.Count == 0)
        {
            slepingZZ.SetActive(true);
            return;
        }
    }

    private void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.TryGetComponent(out StackController stack))
        {
            stack.GetPaper(GetPaperPos(), paperStackPos, onDeskPapers);
        }
    }

 
    private Vector3 GetPaperPos()
    {
        Vector3 firstPaperPos = paperStackPos.transform.position;
        List<Vector3> positions = new List<Vector3>();

        for (int i = 0; i < onDeskPapers.Count; i++)
        {
            var paper = onDeskPapers[i];
            Vector3 newPos = firstPaperPos + Vector3.up * (i * paper.addYPos);
            positions.Add(newPos);
        }

        if (onDeskPapers.Count == 0)
        {
            positions.Add(firstPaperPos);
        }

        Vector3 totalPos = Vector3.zero;
        foreach (Vector3 pos in positions)
        {
            totalPos += pos;
        }

        Vector3 avgPos = totalPos / positions.Count;

        return avgPos;
    }


    private void Work()
    {
        var paper = onDeskPapers[onDeskPapers.Count - 1];
        Destroy(paper.gameObject);
        RemoveList(paper);
        slepingZZ.SetActive(false);
    }

    private IEnumerator CreateMoney()
    {
        yield return new WaitForSeconds(OfficeWorkerManager.Instance.WorkDelay);

        float x = 0f;
        float y = 0f;
        float z = 0f;

        for (int i = 0; i < moneys.Count; i++)
        {
            x = moneys.Count % 3;

            var valueY = moneys.Count / 3;
            y = Mathf.FloorToInt(valueY);

            //var valueZ = moneys.Count % 12;
            //z = valueZ / 4;



        }

        x *= multiplierX;
        y *= multiplierY;
        //z *= multiplierZ;

        var pos = new Vector3(x, y, z);

        var moneyPrefab = Instantiate(money, moneyStackPos.transform.position + pos, Quaternion.identity);
        var moneyScript = moneyPrefab.GetComponent<MoneyController>();
        moneyScript.Init(this);
        AddListMoney(moneyScript);
    }

    private void RemoveList(PaperController paper)
    {
        if (onDeskPapers.Contains(paper))
        {
            onDeskPapers.Remove(paper);
        }
    }
    private void AddList(PaperController paper)
    {
        if (!onDeskPapers.Contains(paper))
        {
            onDeskPapers.Add(paper);
        }
    }
    private void AddListMoney(MoneyController money)
    {
        if (!moneys.Contains(money))
        {
            moneys.Add(money);
        }
    }
    private void RemoveMoneyList(MoneyController money)
    {
        if (moneys.Contains(money))
        {
            moneys.Remove(money);
        }
    }
    public void SetMoneyCount(MoneyController money)
    {
        RemoveMoneyList(money);
    }

    public List<PaperController> GetOnDeskPapers()
    {
        return onDeskPapers;
    }

    #endregion
}
