using System;
using System.Collections;
using System.Collections.Generic;
using ProjectSaga;
using UnityEngine;

public class DamageSys : MonoBehaviour
{
    public static DamageSys Instance { get; private set; }
    [Header ("Object Dealer of Damage")]
    [SerializeField] private GameObject _damageDealer = default;
    public bool _isDead = default;
    
    [Header("Life System")]
    [SerializeField]
    public int _life = default;

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
        for (int i = 0; i < amount; i++)
        {
            _life--;
            if (_life <= 0)
            {
                _isDead = true;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (_damageDealer.CompareTag("DamageDealer"))
        {
            RemovingLife(1);
        }
    }
}
