using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using FishNet.Object;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectSaga
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] public GameObject _LoseCanvas = default;
        public bool isLoseCanvasActive = false;

        private void Awake()
        {
        }
        
        /*public void Start()
        {
            _LoseCanvas = GameObject.FindGameObjectWithTag("LoseCanvas");
        }*/
        

        public void PlayerDeath()
        {
            if (isLoseCanvasActive == false)
            {
                _LoseCanvas.SetActive(true);
            }
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

        
    }    
}

