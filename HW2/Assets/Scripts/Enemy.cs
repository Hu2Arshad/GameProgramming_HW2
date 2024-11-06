using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    private Transform player;
    public float attackRange = 3.0f;
    public float attackCooldown = 3f;
    public float aoeRange = 6.0f;
    public int aoeDamage = 25;

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
    public GameObject bulletPrefab;
    public Transform gunBarrel1;
    public Transform gunBarrel2;
    public Transform gunBarrel3;
    public Transform aoeTransform;
    public float bulletSpeed = 20f;
    private SFXController soundEffect;
    private PlayerHealth playerHP;
    private SFXController playerSFX;

    private ParticleManager aoeEffect;
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
        lastAttackTime = Time.time;

        hpTransform = transform.Find("EnemyHPbar");
        GameObject hpObject = hpTransform.gameObject;
        HPBar = hpObject.GetComponent<EnemyHPBar>();
        registerEnemy = FindObjectOfType<CheckFinished>();
        registerEnemy.AddEnemy();
        soundEffect = GetComponent<SFXController>();
        playerHP = player_obj.GetComponent<PlayerHealth>();
        playerSFX = player_obj.GetComponent<SFXController>();
        aoeEffect = FindObjectOfType<ParticleManager>();
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
        soundEffect.PlayAttack();
        lastAttackTime = Time.time;
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    public void FireballAttack()
    {
        GameObject bullet1 = Instantiate(bulletPrefab, gunBarrel1.position, gunBarrel1.rotation);
        Rigidbody bulletRb1 = bullet1.GetComponent<Rigidbody>();
        if (bulletRb1 != null)
        {
            bulletRb1.velocity = gunBarrel1.forward * bulletSpeed;
        }
        GameObject bullet2 = Instantiate(bulletPrefab, gunBarrel2.position, gunBarrel2.rotation);
        Rigidbody bulletRb2 = bullet2.GetComponent<Rigidbody>();
        if (bulletRb2 != null)
        {
            bulletRb2.velocity = gunBarrel2.forward * bulletSpeed;
        }
        GameObject bullet3 = Instantiate(bulletPrefab, gunBarrel3.position, gunBarrel3.rotation);
        Rigidbody bulletRb3 = bullet3.GetComponent<Rigidbody>();
        if (bulletRb3 != null)
        {
            bulletRb3.velocity = gunBarrel3.forward * bulletSpeed;
        }                
        Destroy(bullet1, 5f);
        Destroy(bullet2, 5f);
        Destroy(bullet3, 5f);
    }

    public void AoeAttack()
    {
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= aoeRange)
        {
            playerHP.Damaged(aoeDamage);
            playerSFX.PlayGotHit();
        }
    }

    public void StartAoe()
    {
        aoeEffect.AoeEffect(transform.position);
    }

    public void Damaged()
    {
        HP -= 10.0f;
        HP = Mathf.Max(HP, 0);
        HPBar.UpdateHP(HP,maxHP);
        soundEffect.PlayGotHit();
        if(HP == 0 && alive)
        {
            Debug.Log("Entered Death");
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
