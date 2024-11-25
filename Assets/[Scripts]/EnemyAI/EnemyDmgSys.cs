using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using ProjectSaga;
using UnityEngine;

public class EnemyDmgSys : NetworkBehaviour
{
    [Header("Object Dealer of Damage")] 
    private readonly SyncVar<GameObject> _dealerofDmg = new SyncVar<GameObject>();

    [Header("Life System")] 
    private readonly SyncVar<int> _life = new SyncVar<int>();

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (_dealerofDmg.Value == null)
        {
            _dealerofDmg.Value = GameObject.FindWithTag("Enemy");
            Debug.Log("Dealer of Damage has been set to" + _dealerofDmg.Value);
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
    
    private void OnTriggerEnter(Collider col)
    {
        if (_dealerofDmg.Value.CompareTag("Enemy"))
        {
            Debug.Log("Player has been hit");
            RemovingLifeServerRPC(1);
        }
    }
}
