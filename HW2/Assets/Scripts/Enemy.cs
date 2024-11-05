using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    private Transform player;
    public float attackRange = 3.0f;
    public float attackCooldown = 3f;

    private NavMeshAgent agent;
    private Animator animator;
    private bool isAttacking;
    private float lastAttackTime;
    private GameObject player_obj;

    public float HP = 40.0f;
    public float maxHP = 40.0f;
    private bool alive = true;
    private Transform hpTransform;
    private EnemyHPBar HPBar;
    private CheckFinished registerEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player_obj = GameObject.Find("Player");
        if(player_obj!=null)
        {
            player = player_obj.GetComponent<Transform>();
        }
        else
        {
            Debug.Log("Cant find player");
        }
        isAttacking = false;
        lastAttackTime = -attackCooldown;

        hpTransform = transform.Find("EnemyHPbar");
        GameObject hpObject = hpTransform.gameObject;
        HPBar = hpObject.GetComponent<EnemyHPBar>();
        registerEnemy = FindObjectOfType<CheckFinished>();
        registerEnemy.AddEnemy();
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

    public void Damaged()
    {
        HP -= 10.0f;
        HP = Mathf.Max(HP, 0);
        HPBar.UpdateHP(HP,maxHP);
        
        if(HP == 0 && alive)
        {
            registerEnemy.RemoveEnemy();
            agent.isStopped = true;
            animator.SetTrigger("Dead");
            Destroy(gameObject, 1);
            alive = false;
            
        }
        else if(!isAttacking)
        {
            
            animator.SetTrigger("Hit");
        }
    }

    public float GetHP()
    {
        return HP;
    }

    public float GetMaxHP()
    {
        return maxHP;
    }

}
