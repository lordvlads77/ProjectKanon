using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSys : MonoBehaviour
{
    [SerializeField] private GameObject _pickable = default;
    [SerializeField] private GameObject _objToSpawn = default;
    private void OnTriggerEnter(Collider other)
    {
        _pickable.SetActive(false);
        _objToSpawn.SetActive(true);
    }
}
