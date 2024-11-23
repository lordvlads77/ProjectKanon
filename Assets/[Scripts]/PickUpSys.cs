using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class PickUpSys : NetworkBehaviour
{
    [SerializeField] private GameObject _pickable = default;
    public bool _isPicked = false;

    [Server]
    public void OnTriggerEnter(Collider other)
    {
        Despawn(_pickable);
        _isPicked = true;
    }
}
