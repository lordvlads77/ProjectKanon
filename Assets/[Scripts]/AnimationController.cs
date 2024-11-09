using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSaga
{
    public class AnimationController : MonoBehaviour
    {
        public static AnimationController Instance { get; private set; }
        [SerializeField] private Animator _animator = default;
        [SerializeField] private Animator _zombieAnimator = default;
        private readonly int _IsMoving = Animator.StringToHash("isMoving");
        private readonly int _Withdrawing = Animator.StringToHash("Withdrawing");
        private readonly int _attacking = Animator.StringToHash("Attacking");
        private readonly int _isSwordRunning = Animator.StringToHash("isSwordRunning");
        private readonly int _jumping = Animator.StringToHash("jumping");
        private readonly int _sheating = Animator.StringToHash("Sheathing");
        private readonly int _swordJumping = Animator.StringToHash("SwordJump");
        private readonly int _speed = Animator.StringToHash("speed");
        private readonly int _zombieAttack = Animator.StringToHash("zombieAttack");
        

        public void Awake()
        {
            Instance = this;
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void Moving()
        {
            _animator.SetBool(_IsMoving, true);
        }

        public void notMoving()
        {
            _animator.SetBool(_IsMoving, false);
        }

        public void jumping()
        {
            _animator.SetTrigger(_jumping);
        }

        public void WithdrawingWeapon()
        {
            _animator.SetTrigger(_Withdrawing);
        }

        public void WeaponSlash()
        {
            _animator.SetTrigger(_attacking);
        }

        public void SwordRun()
        {
            _animator.SetBool(_isSwordRunning, true);
        }
        
        public void notSwordRun()
        {
            _animator.SetBool(_isSwordRunning, false);
        }

        public void Sheating()
        {
            _animator.SetTrigger(_sheating);
        }
        
        public void SwordJumping()
        {
            _animator.SetTrigger(_swordJumping);
        }

        public void ZombieMove()
        {
            _zombieAnimator.SetFloat(_speed, 1);
        }
        
        public void ZombieAttack()
        {
            _zombieAnimator.SetTrigger(_zombieAttack);
        }
    }
}