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
    private readonly SyncVar<int> _life = new SyncVar<int>();

    public override void OnStartServer()
    {
        base.OnStartServer();
        if (_dealerofDmg == null)
        {
            GameObject enemy = GameObject.FindWithTag("Enemy"); // Find the Enemy object
            if (enemy != null)
            {
                _dealerofDmg.Value = enemy;
                Debug.Log("Dealer of Damage set to: " + _dealerofDmg.Value);
            }
            else
            {
                Debug.LogError("Enemy object not found in the scene!");
            }
        }
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

    [ServerRpc]
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
            //TODO: Discover how to work with syncvars so I can change the value of life so this code works
        }
    }
    
    /*private void OnTriggerEnter(Collider col)
    {
        if (_dealerofDmg.Value.CompareTag("Player"))
        {
            Debug.Log("Player has been hit");
            RemovingLifeServerRPC(1);
        }
        //TODO: Check this with a Collision Enter instead of being a trigger, if this fails check Nick's Latest Solution
    }*/
}
