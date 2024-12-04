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
        private readonly int _isSwordRunning = Animator.StringToHash("isSwordRunning");
        private readonly int _isAttacking = Animator.StringToHash("isAttacking");
        private readonly int _isWithdrawing = Animator.StringToHash("isWithdrawing");
        private readonly int _isJumping = Animator.StringToHash("isJumping");
        private readonly int _isSheathing = Animator.StringToHash("isSheathing");
        private readonly int _isSwordJumping = Animator.StringToHash("isSwordJumping");


        public void Moving()
        {
            _animator.SetBool(_IsMoving, true);
        }

        public void notMoving()
        {
            _animator.SetBool(_IsMoving, false);
        }

        public void Jumping()
        {
            _animator.SetBool(_isJumping, true);
        }
        
        public void NotJumping()
        {
            _animator.SetBool(_isJumping, false);
        }

        public void WithdrawingWeapon()
        {
            _animator.SetBool(_isWithdrawing, true);
        }
        public void NotWithdrawing()
        {
            _animator.SetBool(_isWithdrawing, false);
        }

        public void WeaponSlash()
        {
            _animator.SetBool(_isAttacking, true);
        }

        public void FinishedSlashing()
        {
            _animator.SetBool(_isAttacking, false);
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
            _animator.SetBool(_isSheathing, true);
        }
        
        public void FinishedSheathing()
        {
            _animator.SetBool(_isSheathing, false);
        }
        
        public void SwordJumping()
        {
            _animator.SetBool(_isSwordJumping, true);
        }
        
        public void NotSwordJumping()
        {
            _animator.SetBool(_isSwordJumping, false);
        }
    }
}