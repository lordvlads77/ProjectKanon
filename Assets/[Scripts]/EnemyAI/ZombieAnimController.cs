using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class ZombieAnimController : NetworkBehaviour
{
    public static ZombieAnimController Instance { get; private set; }
    [SerializeField] private Animator _zombieAnimator = default;
    private readonly int _zombiespeed = Animator.StringToHash("speed");
    private readonly int _zombieAttack = Animator.StringToHash("zombieAttack");
    private readonly int _zombieDeath = Animator.StringToHash("zombieDead");


    private void Awake()
    {
        Instance = this;
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ZombieMove()
    {
        _zombieAnimator.SetFloat(_zombiespeed, 1f);
    }
    
    public void ZombieAttack()
    {
        _zombieAnimator.SetTrigger(_zombieAttack);
    }

    public void ZombieDeath()
    {
        _zombieAnimator.SetBool(_zombieDeath, true);
    }
}
