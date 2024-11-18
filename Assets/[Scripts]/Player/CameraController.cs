/*using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class CameraController : NetworkBehaviour
{
    //readonly SyncVar<CinemachineVirtualCamera> _virtualCamera = new SyncVar<CinemachineVirtualCamera>();
    readonly SyncVar<GameObject> _followCamera = new SyncVar<GameObject>();

    public override void OnStartNetwork() 
    {
        base.OnStartNetwork();
        if (Owner.IsLocalClient)
        {
            AssignVirtualCameraServer();
            EnableCamera();
        }
        else
        {
            DisableCamera();
        }
    }
    
    [ServerRpc] // Una funcion que se ejecuta en el servidor
    private void AssignVirtualCameraServer()
    {
        Debug.Log("Se asigno la camara virtual");
        if (IsOwner == false)
        {
            return;
        }
        //GameObject _followCamera = GameObject.FindWithTag("FollowCamera");
        _followCamera.NetworkBehaviour = GameObject.FindWithTag("FollowCamera");
        if (_followCamera != null)
        {
            _virtualCamera.NetworkBehaviour = _followCamera.NetworkBehaviour.GetComponent<CinemachineVirtualCamera>();
            if (_virtualCamera != null)
            {
                _virtualCamera.NetworkBehaviour.Follow = transform;
                _virtualCamera.NetworkBehaviour.LookAt = transform;
                Debug.Log($"Camera assigned to {gameObject.name}");
            }
            else
            {
                Debug.LogError("Cinemachine Virtual Camera component not found in the FollowCamera object.");
            }
        }
        else
        {
            Debug.LogError("FollowCamera object not found in the scene.");
        }
    }
    [ServerRpc] // Una funcion que se ejecuta en el servidor
    private void EnableCamera()
    {
        if (IsOwner == false)
        {
            return;
        }
        if (_virtualCamera != null)
        {
            _virtualCamera.NetworkBehaviour.gameObject.SetActive(true);
            _virtualCamera.NetworkBehaviour.Priority = 10;
            Debug.Log($"Camera enabled for {gameObject.name}");
        }
    }
    
    [ServerRpc]
    private void DisableCamera()
    {
        if (IsOwner == false)
        {
            return;
        }
        if (_virtualCamera != null)
        {
            _virtualCamera.NetworkBehaviour.gameObject.SetActive(false);
            //_virtualCamera.NetworkBehaviour.Priority = 0;
            Debug.Log($"Camera disabled for {gameObject.name}");
        }
    }
}*/
