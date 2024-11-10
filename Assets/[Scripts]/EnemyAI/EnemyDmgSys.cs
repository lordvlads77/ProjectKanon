using System.Collections;
using System.Collections.Generic;
using ProjectSaga;
using UnityEngine;

public class EnemyDmgSys : MonoBehaviour
{
    [Header("Object Dealer of Damage")]
    [SerializeField] private GameObject _dealerofDmg = default;
    
    [Header("Life System")]
    [SerializeField] private int _life = default;
    
    public void RemovingLife(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            _life--;
            if (_life <= 0)
            {
                GameManager.Instance.PlayerDeath();
            }
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
