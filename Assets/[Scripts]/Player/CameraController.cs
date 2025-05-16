using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera = default;

    public void Awake()
    {
        AssignVirtualCamera();
        EnableCamera();
    }


    private void AssignVirtualCamera()
    {
        _virtualCamera.Follow = transform;
        _virtualCamera.LookAt = transform;
        Debug.Log($"Camera assigned to {gameObject.name}");
    }
    private void EnableCamera()
    {
        if (_virtualCamera != null)
        {
            _virtualCamera.gameObject.SetActive(true);
            _virtualCamera.Priority = 10;
            Debug.Log($"Camera enabled for {gameObject.name}");
        }
    }
    
    private void DisableCamera()
    {
        //TODO: Maybe call this function on the death event?
        if (_virtualCamera != null)
        {
            _virtualCamera.gameObject.SetActive(false);
            _virtualCamera.Priority = 0;
            Debug.Log($"Camera disabled for {gameObject.name}");
        }
    }
}
