using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSys : MonoBehaviour
{
    [SerializeField] private GameObject _pickable = default;
    public bool _isPicked = false;
    public GameObject _winCanvas;
    
    public void OnTriggerEnter(Collider other)
    {
        StartCoroutine(WinSequence());
    }

    IEnumerator WinSequence()
    {
        _winCanvas.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        _pickable.SetActive(false);
        _isPicked = true;
    }
}
