using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using ProjectSaga;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyAI : NetworkBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatisGround;
    public LayerMask whatisPlayer;


    [Header("Patrolling")] 
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    
    [Header("Attacking")]
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    [Header("States")]
    public float sightRange;
    public float attackRange;
    public bool playerInSightRange;
    public bool playerInAttackRange;
    
    [Header("Death Event")]
    [SerializeField] private GameObject _deathObj = default;
    
    public DamageSys damageSys;

    [FormerlySerializedAs("ZombieAnimController")] public ZombieAnimController zombieAnimController;

    public override void OnStartNetwork()
    {
        agent = GetComponent<NavMeshAgent>();
        if (IsServerStarted == false)
        {
            agent.enabled = false;
        }
        //player = GameObject.FindWithTag("Player").transform;
        //Ya no se hara el script de la lista de players porque tiempo
    }
    
    

    private void Update()
    {
        if (IsServerStarted == false)
        {
            return;
        }
        //Check for sight and attack Range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();   
        }
        if (playerInAttackRange && playerInSightRange && damageSys._isDead == true)
        {
            Dying();
        }
    }

    private void Patroling()
    {
        zombieAnimController.ZombieStopAttack();
        zombieAnimController.ZombieMove();
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint Reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        // Calculate Random Point in Range;
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatisGround))
        {
            walkPointSet = true;
        }

    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        zombieAnimController.ZombieMove();
    }

    private void AttackPlayer()
    {
        // Make sure enemy does not move
        agent.SetDestination(transform.position);
        //transform.LookAt(player);

        if (!alreadyAttacked)
        {
            zombieAnimController.ZombieAttack();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void Dying()
    {
        zombieAnimController.ZombieStopAttack();
        StartCoroutine(Death());
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    IEnumerator Death()
    {
        zombieAnimController.ZombieDeath();
        agent.isStopped = true;
        yield return new WaitForSeconds(2.6f);
        Destroy(_deathObj);
    }
}
