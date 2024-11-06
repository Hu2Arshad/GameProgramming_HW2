using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject Particle_HitEffect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called to instantiate hit effect at effect_pos, as a child to parentTr if provided
    public void HitEffect(Vector3 effect_pos, Transform parentTr = null)
    {
        if (Particle_HitEffect == null)
        {
            Debug.Log("No HitEffect Particle set to ParticleManager");
            return;
        }

        GameObject prefab;
        if (parentTr == null) prefab = Instantiate(Particle_HitEffect, effect_pos, Quaternion.identity);
        else prefab = Instantiate(Particle_HitEffect, effect_pos, Quaternion.identity, parentTr);

        Destroy(prefab, 1.0f);
    }
}
