using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSaga
{
    public class SoundController : MonoBehaviour
    {
        public static SoundController Instance { get; private set; }
        [SerializeField] private AudioSource _medievalMusic = default;

        private void Awake()
        {
            Instance = this;
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void GeneralMusic()
        {
            _medievalMusic.Play();
        }
    }    
}

