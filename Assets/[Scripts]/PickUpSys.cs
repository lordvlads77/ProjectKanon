using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class PickUpSys : NetworkBehaviour
{
    [SerializeField] private GameObject _pickable = default;
    public bool _isPicked = false;
    public GameObject _winCanvas;

    [Server]
    public void OnTriggerEnter(Collider other)
    {
        StartCoroutine(WinSequence());
    }

    IEnumerator WinSequence()
    {
        _winCanvas.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Despawn(_pickable);
        _isPicked = true;
    }
}
