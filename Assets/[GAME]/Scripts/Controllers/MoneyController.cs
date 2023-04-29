using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using OfficeFever.Managers;
public class MoneyController : MonoBehaviour
{
    #region Variables

    private PlayerController player;

    [SerializeField] private OfficeWorkerController officeWorkerController;
    [SerializeField] private float duration;
    [SerializeField] private float moveDuration;
    [SerializeField] private float moneyValue;

    private bool isTrigger;
    
    #endregion
    #region Methods


    
    public void Init(OfficeWorkerController worker)
    {
        officeWorkerController = worker;
    }
    private void Start()
    {
        player = PlayerManager.Instance.GetPlayer();
        SetCreateShake();
    }
    private void SetCreateShake()
    {
        transform.DOShakeScale(duration, .35f, 1, 1);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.TryGetComponent(out PlayerController player))
    //    {

    //    }
    //}

  

    public void DOMove(Transform player)
    {
        Sequence s = DOTween.Sequence();
        transform.parent = player.transform;
        s.Append(transform.DOLocalJump(Vector3.up * 1.5f, 1, 1, moveDuration)).SetEase(Ease.Linear).SetUpdate(true);
        s.Join(transform.DORotate(transform.up * 360, moveDuration, RotateMode.Fast));
        s.AppendInterval(moveDuration - .25f);
        s.Join(transform.DOScale(Vector3.one / 10f, .25f));
        s.OnComplete(() =>
        {
            GameManager.Instance.SetTotalMoney(moneyValue);
            UIManager.Instance.UpdateTotalMoney(GameManager.Instance.TotalMoney);
            Destroy(this.gameObject);

        });
        officeWorkerController.SetMoneyCount(this);
    }
    #endregion
}
