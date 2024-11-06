using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public AudioClip attackSound;
    public AudioClip gotHit;
    public AudioClip deathSound;
    public AudioClip healedSound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = 0.25f;
    }

    public void PlayAttack()
    {
        audioSource.PlayOneShot(attackSound);
    }

    public void PlayGotHit()
    {
        audioSource.PlayOneShot(gotHit);
    }

    public void PlayDeath()
    {
        audioSource.PlayOneShot(deathSound);
    }

    public void PlayHealed()
    {
        audioSource.PlayOneShot(healedSound);
    }
}
