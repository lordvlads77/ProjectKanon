using System;
using System.Collections;
using ProjectSaga;
using UnityEngine;

public class EnemyDmgSys : MonoBehaviour
{
    [Header("Object Dealer of Damage")] 
    [SerializeField] private GameObject _dealerofDmg = default;

    [Header("Life System")] 
    private int _life = 100;

    [Header("Dmg Logic")] 
    public float damageInterval = 100f;
    public bool canDoDmg;

    public GameObject _playerObj;

    public GameManager gameManager;

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

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    
    public void RemovingLifeServerRPC(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            _life = Mathf.Clamp(_life, 0, 100);
            _life--;
            Debug.Log("Life has been removed " + _life);
            if (_life <= 0)
            {
                StartCoroutine(DeathScreen());
                break;
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player has been hit");
            RemovingLifeServerRPC(1);
            StartCoroutine(DamageCooldown());
        }
    }

    /*public override void OnStopClient()
    {
        if (IsOwner == false)
        {
            return;
        }
        base.OnStopClient();
        StartCoroutine(DeathScreen());
    }*/

    private void OnDestroy()
    {
        
    }

    IEnumerator DamageCooldown()
    {
        canDoDmg = false;
        yield return new WaitForSeconds(damageInterval);
        canDoDmg = true;
    }

    IEnumerator DeathScreen()
    {
        //_youlosecanvas.SetActive(true);
        gameManager.PlayerDeath();
        yield return new WaitForSeconds(0.3f);
        gameManager.isLoseCanvasActive = false;
    }
}
