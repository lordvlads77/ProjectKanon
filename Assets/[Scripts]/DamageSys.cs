using System;
using System.Collections;
using System.Collections.Generic;
using ProjectSaga;
using UnityEngine;

public class DamageSys : MonoBehaviour
{
    [Header ("Object Dealer of Damage")]
    [SerializeField] private GameObject _damageDealer = default;
    
    [Header("Life System")]
    [SerializeField] private int _life = default;

    public void RemovingLife(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            _life--;
            if (_life <= 0)
            {
                GameManager.Instance.Death();
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
