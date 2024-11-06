using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private ParticleManager particleManager;
    // Start is called before the first frame update
    void Start()
    {
        particleManager = GameObject.Find("ParticleManager").GetComponent<ParticleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collided)
    {
        if (collided.transform.tag == "Breakables" || collided.transform.tag == "Enemy")
        {
            //Hit Particle Effect
            if (particleManager != null) particleManager.HitEffect(transform.position, collided.transform);
            else Debug.Log("Bullet Unable to locate ParticleManager");
        }
        else Destroy(gameObject);
    }
}
