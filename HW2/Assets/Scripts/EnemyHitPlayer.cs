using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitPlayer : MonoBehaviour
{
    public int damage = 10;

    private PlayerHealth playerHP;
    private float lastAttackTime = -Mathf.Infinity;
    public float cooldownTime = 1f;
    private Enemy this_parent;
    // Start is called before the first frame update
    void Start()
    {
        playerHP = GameObject.Find("Player").GetComponent<PlayerHealth>();
        this_parent = GetComponentInParent<Enemy>();

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collided)
    {
        if(collided.tag == "Player" && Time.time >= lastAttackTime + cooldownTime && this_parent.GetHP() > 0.0f)
        {
            playerHP.Damaged(damage);
            lastAttackTime = Time.time;
        }
        if(collided.tag == "GunBullet")
        {
            this_parent.Damaged();
            Destroy(collided.gameObject);
        }
    }
}
