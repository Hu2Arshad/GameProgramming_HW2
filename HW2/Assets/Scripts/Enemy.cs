using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public Transform player;
    public float attackRange = 3.0f;
    public float attackCooldown = 3f;

    private NavMeshAgent agent;
    private Animator animator;
    private bool isAttacking;
    private float lastAttackTime;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        isAttacking = false;
        lastAttackTime = -attackCooldown;

    }

    void Update()
    {
       float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }
        else if (!isAttacking)
        {
            Chase();
        }        
    }

    void Chase()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
        animator.SetBool("Moving", true);
    }

    void Attack()
    {
        isAttacking = true;
        agent.isStopped = true;
        animator.SetBool("Moving", false);
        animator.SetTrigger("Attack");
        lastAttackTime = Time.time;
    }

    public void EndAttack()
    {
        isAttacking = false;
    }



}
