using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class ZombieAnimController : NetworkBehaviour
{
    [SerializeField] private Animator _zombieAnimator = default;
    private readonly int _zombiespeed = Animator.StringToHash("speed");
    private readonly int _zombieAttack = Animator.StringToHash("zombieAtk");
    private readonly int _zombieDeath = Animator.StringToHash("zombieDead");

    public void ZombieMove()
    {
        _zombieAnimator.SetFloat(_zombiespeed, 1f);
    }
    
    public void ZombieAttack()
    {
        _zombieAnimator.SetBool(_zombieAttack, true);
    }
    
    public void ZombieStopAttack()
    {
        _zombieAnimator.SetBool(_zombieAttack, false);
    }

    public void ZombieDeath()
    {
        _zombieAnimator.SetBool(_zombieDeath, true);
    }
}
