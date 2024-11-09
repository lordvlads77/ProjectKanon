using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectSaga
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        [SerializeField] private GameObject _deathActor = default;

        private void Awake()
        {
            Instance = this;
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void Death()
        {
            if (_deathActor.CompareTag("Player"))
            {
                PlayerDeath();
            }
            else if (_deathActor.CompareTag("Enemy"))
            {
                EnemyDeath();
            }
        }

        public void PlayerDeath()
        {
            //TODO: Add Whatever needs to be added here in the future
        }

        public void EnemyDeath()
        {
            //TODO: Add Whatever needs to be added here in the future
        }

        public void PlayGame()
        {
            //GUI.Instance.playButton();
        }

        public void Start()
        {
            //SoundController.Instance.GeneralMusic();
        }
        
    }    
}

