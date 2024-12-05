using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectSaga
{
    public class GUI : NetworkBehaviour
    {
        public static GUI Instance { get; private set; }
        public GameObject _losecanvas = default;
        public GameObject _player = default;

        private void Awake()
        {
            Instance = this;
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        

        public void playButton()
        {
            
        }

        public void ReturnToMainMenu()
        {
            //Despawn(_player, DespawnType.Destroy);
            //_losecanvas.SetActive(false);
        }
    }   
}
