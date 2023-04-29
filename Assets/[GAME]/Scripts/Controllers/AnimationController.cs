using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    #region Variables
   [SerializeField] private Animator _animator;
    #endregion


    
    public void OnWalkAnimation(float speed)
    {
        _animator.SetFloat(AnimationState.speed, speed);
    }

}