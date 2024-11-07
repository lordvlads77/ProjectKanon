using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUI = ProjectSaga.GUI;

public class Moricion : MonoBehaviour
{
    [SerializeField] private GameObject _deathObject = default;
    [SerializeField] private GameObject _playerRef = default;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(_playerRef);
        GUI.Instance.deathScreen();
    }
}
