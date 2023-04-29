using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PaperController : MonoBehaviour
{
    #region Variables
    public float addYPos;
    private Sequence _sequence;
    private Sequence _sequencePaper;
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve easeJump;
    #endregion
    #region Methods

    public void MoveTarget(Vector3 pos, Transform target)
    {
        this.transform.parent = target.transform;
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOJump(pos, 1, 1, duration).SetEase(easeJump));
        _sequence.Join(transform.DORotate(Vector3.up * 360, duration, RotateMode.WorldAxisAdd).SetEase(easeJump));
        _sequence.OnComplete(() => { transform.DOShakeScale(duration, .35f, 1, 1); });
        
    }
    public void MoveTargetPlayer(Transform target, Vector3 pos)
    {
        this.transform.parent = target.transform;
        _sequence.Kill(true);
        _sequencePaper = DOTween.Sequence();
        _sequencePaper.Append(transform.DOLocalJump(pos, 1, 1, duration).SetEase(easeJump));
    }

    public void MoveDesk(Transform parent,Vector3 pos)
    {
        this.transform.parent = parent.transform;
        transform.DOJump(pos, 1, 1, duration).SetEase(easeJump);
    }
    #endregion

}
