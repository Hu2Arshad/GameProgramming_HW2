using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Viapix_PlayerParams;

namespace Viapix_HealingItem
{
    public class Viapix_HealingItem : MonoBehaviour
    {
        [SerializeField]
        float rotationSpeedX, rotationSpeedY, rotationSpeedZ;

        [SerializeField]
        int healingAmount;

        GameObject playerObj;

        private void Start()
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            transform.Rotate(rotationSpeedX, rotationSpeedY, rotationSpeedZ);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                

                
            }
        }
    }
}

