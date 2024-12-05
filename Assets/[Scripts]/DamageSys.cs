using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using ProjectSaga;
using UnityEngine;

public class DamageSys : NetworkBehaviour
{
    public static DamageSys Instance { get; private set; }
    [Header ("Object Dealer of Damage")]
    [SerializeField] private GameObject _damageDealer = default;
    public bool _isDead = default;
    public ProjectSaga.SFXController sfxController;
    
    [Header("Life System")]
    [SerializeField]
    public readonly SyncVar<int> _life = new (10);

    private void Awake()
    {
        Instance = this;
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    public void RemovingLife(int amount)
    {
        sfxController.SwordHit();
        for (int i = 0; i < amount; i++)
        {
            _life.Value--;
            if (_life.Value <= 0)
            {
                _isDead = true;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!IsServerStarted)
        {
            return;
        }
        if (_damageDealer.CompareTag("DamageDealer"))
        {
            RemovingLife(1);
            Debug.Log("Enemy has been hit");
        }
    }
}
