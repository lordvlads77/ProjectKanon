using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using ProjectSaga;
using UnityEngine;

public class EnemyDmgSys : NetworkBehaviour
{
    [Header("Object Dealer of Damage")]
    [SerializeField] private GameObject _dealerofDmg = default;

    [Header("Life System")] 
    private readonly SyncVar<int> _life = new SyncVar<int>();
    
    [Server]
    public void RemovingLife(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            //_life--;
            /*if (_life <= 0)
            {
                GameManager.Instance.PlayerDeath();
            }*/
            //TODO: Discover how to work with syncvars so I can change the value of life so this code works
        }
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (_dealerofDmg.CompareTag("DamageDealer"))
        {
            RemovingLife(1);
        }
    }
}
