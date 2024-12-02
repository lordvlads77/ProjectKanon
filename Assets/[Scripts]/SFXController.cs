using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSaga
{
    public class SFXController : MonoBehaviour
    {
        [SerializeField] private AudioSource _SFXSource = default;

        [SerializeField] private AudioClip _swordSwing = default;
        [SerializeField] private AudioClip _swordSheath = default;
        [SerializeField] private AudioClip _swordHit = default;

        public void SwordSwing()
        {
            _SFXSource.PlayOneShot(_swordSwing, 1f);
        }

        public void SwordSheath()
        {
            _SFXSource.PlayOneShot(_swordSheath, 1f);
        }

        public void SwordHit()
        {
            _SFXSource.PlayOneShot(_swordHit, 1f);
        }


    }

}