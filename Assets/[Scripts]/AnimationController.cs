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
        private readonly int _IsMoving = Animator.StringToHash("isMoving");
        private readonly int _isJumping = Animator.StringToHash("isJumping");
        private readonly int _Withdrawing = Animator.StringToHash("Withdrawing");

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
            _animator.SetBool(_isJumping, true);
        }

        public void notJumping()
        {
            _animator.SetBool(_isJumping, false);
        }

        public void WithdrawingWeapon()
        {
            _animator.SetTrigger(_Withdrawing);
        }
    }
}