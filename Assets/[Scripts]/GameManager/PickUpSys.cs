using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PickUpSys : MonoBehaviour
{
    public static PickUpSys Instance { get; private set; }
    [SerializeField] private GameObject _pickable = default;
    [FormerlySerializedAs("_swordSheathfbx")] [SerializeField] private GameObject _swordSheatheadfbx = default;
    public bool _isPicked = false;

    private void Awake()
    {
        Instance = this;    
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        _pickable.SetActive(false);
        _swordSheatheadfbx.SetActive(true);
        _isPicked = true;
    }
}
