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

