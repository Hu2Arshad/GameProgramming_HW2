using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitPlayer : MonoBehaviour
{
    public int damage = 10;

    private PlayerHealth playerHP;
    private GameObject manager;
    private float lastAttackTime = -Mathf.Infinity;
    public float cooldownTime = 1f;
    private HpBar HpSprite;
    private Enemy this_parent;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager");
        playerHP = manager.GetComponent<PlayerHealth>();
        HpSprite = FindObjectOfType<HpBar>();
        this_parent = GetComponentInParent<Enemy>();

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collided)
    {
        if(collided.tag == "Player" && Time.time >= lastAttackTime + cooldownTime)
        {
            playerHP.Damaged(damage);
            HpSprite.UpdateHP(playerHP.GetHP(), playerHP.GetMaxHP());
            lastAttackTime = Time.time;
        }
        if(collided.tag == "GunBullet")
        {
            this_parent.Damaged();
            Destroy(collided.gameObject);
        }
    }
}
