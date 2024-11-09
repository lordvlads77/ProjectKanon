using System;
using System.Collections;
using System.Collections.Generic;
using ProjectSaga;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
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

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
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
    }

    private void Patroling()
    {
        
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
        AnimationController.Instance.ZombieMove();
    }

    private void AttackPlayer()
    {
        // Make sure enemy does not move
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            AnimationController.Instance.ZombieAttack();
            Debug.Log("Attacked");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
