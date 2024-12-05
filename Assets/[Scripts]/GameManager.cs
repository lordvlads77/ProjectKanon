using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using FishNet.Object;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectSaga
{
    public class GameManager : NetworkBehaviour
    {
        public static GameManager Instance { get; private set; }
        [SerializeField] private GameObject _LoseCanvas = default;
        [SerializeField] private GameObject _player = default;
        
        private void Awake()
        {
            Instance = this;
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        public void PlayerDeath()
        {
            _LoseCanvas.SetActive(true);
            Despawn(_player);
            //TODO: Add Whatever needs to be added here in the future
            Debug.Log("Player is Dead");
        }

        public void EnemyDeath()
        {
            Debug.Log("Enemy is Dead");
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

