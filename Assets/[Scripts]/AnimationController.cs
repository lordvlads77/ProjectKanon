using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

namespace ProjectSaga
{
    public class AnimationController : NetworkBehaviour
    {
        [SerializeField] private Animator _animator = default;
        private readonly int _IsMoving = Animator.StringToHash("isMoving");
        private readonly int _Withdrawing = Animator.StringToHash("Withdrawing");
        private readonly int _attacking = Animator.StringToHash("Attacking");
        private readonly int _isSwordRunning = Animator.StringToHash("isSwordRunning");
        private readonly int _jumping = Animator.StringToHash("jumping");
        private readonly int _sheating = Animator.StringToHash("Sheathing");
        private readonly int _swordJumping = Animator.StringToHash("SwordJump");
        

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
    }
}