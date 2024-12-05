using System.Collections;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using ProjectSaga;
using UnityEngine;

public class EnemyDmgSys : NetworkBehaviour
{
    [Header("Object Dealer of Damage")] 
    [SerializeField] private readonly SyncVar<GameObject> _dealerofDmg = new SyncVar<GameObject>();

    [Header("Life System")] 
    private readonly SyncVar<int> _life = new SyncVar<int>(100);

    [Header("Dmg Logic")] 
    public float damageInterval = 100f;
    public bool canDoDmg;

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
                break;
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (!IsServerStarted)
        {
            return;
        }
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player has been hit");
            RemovingLifeServerRPC(1);
            StartCoroutine(DamageCooldown());
        }
    }

    IEnumerator DamageCooldown()
    {
        canDoDmg = false;
        yield return new WaitForSeconds(damageInterval);
        canDoDmg = true;
    }
}
