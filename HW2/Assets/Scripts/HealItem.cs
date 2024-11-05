using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    [SerializeField]
    float rotationSpeedX, rotationSpeedY, rotationSpeedZ;

    [SerializeField]
    int healingAmount = 10;

    private PlayerHealth playerHP;
    // Start is called before the first frame update
    void Start()
    {
        playerHP = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        transform.Rotate(rotationSpeedX, rotationSpeedY, rotationSpeedZ);
    }
    
    void OnTriggerEnter(Collider collided)
    {
        if(collided.tag == "Player")
        {
            playerHP.Healed(healingAmount);
            Destroy(gameObject);
        }
    }
}
