using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletHitPlayer : MonoBehaviour
{

    public int damage = 20;
    private PlayerHealth playerHP;
    private SFXController playerSFX;
    private GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Player");
        playerHP = manager.GetComponent<PlayerHealth>();    
        playerSFX = GameObject.Find("Player").GetComponent<SFXController>();  
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collided)
    {
        if(collided.tag == "Player")
        {
            playerHP.Damaged(damage);
            playerSFX.PlayGotHit();
            Destroy(gameObject);
        }
        if(collided.tag == "Untagged")
        {
            Destroy(gameObject);
        }
    }
}
