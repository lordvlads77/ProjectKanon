using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using ProjectSaga;
using UnityEngine;

public class EnemyDmgSys : NetworkBehaviour
{
    [Header("Object Dealer of Damage")] 
    [SerializeField] private readonly SyncVar<GameObject> _dealerofDmg = new SyncVar<GameObject>();

    [Header("Life System")] 
    private readonly SyncVar<int> _life = new SyncVar<int>(10);

    public override void OnStartServer()
    {
        base.OnStartServer();
    }

    /*public override void OnSpawnServer(NetworkConnection connection)
    {
        base.OnSpawnServer(connection);
        if (_dealerofDmg.Value != null)
        {
            Debug.Log("No es igual a null");
        }
        else
        {
            _dealerofDmg.Value = GameObject.FindWithTag("Enemy");
            Debug.Log("Dealer of Damage has been set to" + _dealerofDmg.Value);
        }
        
    }*/

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (_dealerofDmg != null)
        {
            Debug.Log("Client received Dealer of Damage: " + _dealerofDmg.Value);
        }
    }
    
    public void RemovingLifeServerRPC(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            _life.Value--;
            Debug.Log("Life has been removed " + _life.Value);
            if (_life.Value <= 0)
            {
                GameManager.Instance.PlayerDeath();
            }
        }
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (!IsServerStarted)
        {
            return;
        }
        if (col.CompareTag("Enemy"))
        {
            Debug.Log("Player has been hit");
            RemovingLifeServerRPC(1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //TODO: Do a timer so I wont remove life each eand every frame
    }
}
