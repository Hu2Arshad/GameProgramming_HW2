using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletHitPlayer : MonoBehaviour
{

    public int damage = 20;
    private PlayerHealth playerHP;
    private GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Player");
        playerHP = manager.GetComponent<PlayerHealth>();      
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collided)
    {
        if(collided.tag == "Player")
        {
            playerHP.Damaged(damage);
            Destroy(gameObject);
        }
    }
}
