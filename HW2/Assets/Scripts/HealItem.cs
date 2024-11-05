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
    private GameObject manager;
    private HpBar HpSprite;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager");
        playerHP = manager.GetComponent<PlayerHealth>();
        HpSprite = FindObjectOfType<HpBar>();
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
            HpSprite.UpdateHP(playerHP.GetHP(), playerHP.GetMaxHP());
            Destroy(gameObject);
        }
    }
}
