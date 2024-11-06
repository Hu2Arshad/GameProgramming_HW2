using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject broken;
    public GameObject heal;
    private MeshRenderer meshRenderer;
    private Collider[] colliders;
    private AudioSource audioSource;
    public AudioClip breaking;
    Vector3 healPosOffset = new Vector3(0.0f, 2.0f, 0.0f);
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        colliders = GetComponents<Collider>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "GunBullet")
        {
            Destroy(other.gameObject);
            GameObject brokenObj = Instantiate(broken, transform.position, transform.rotation);
            GameObject healItem = Instantiate(heal, transform.position + healPosOffset, transform.rotation);
            audioSource.PlayOneShot(breaking);
            Destroy(brokenObj, 2);
            Destroy(gameObject, 2);
            meshRenderer.enabled = false;
            foreach (Collider col in colliders)
            {
                col.enabled = false;
            }
        }
    }
}
