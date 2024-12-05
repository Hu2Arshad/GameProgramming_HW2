using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFinished : MonoBehaviour
{
    private int enemiesLeft = 0;
    private GameObject portal;
    private MeshRenderer meshRenderer;
    private Collider colliders;

    private Enemy enemy;

    [Header("Spawner Settings")]
    public int enemiesToKill = 5;
    public int maxEnemiesOnScreen = 5;
    private int enemiesDefeated = 0;

    public AudioClip portalOpenSFX;
    private AudioSource audioSource;

    private bool portalActive = false; 

    private UpdateText objectiveUI;
    void Start()
    {
        portal = GameObject.Find("Portal");
        if(portal != null)
        {
            //meshRenderer = portal.GetComponent<MeshRenderer>();
            //colliders = portal.GetComponent<Collider>();
            portal.SetActive(false);
        }
        else
        {
            Debug.Log("cant find portal obj");
        }
        //meshRenderer.enabled = false;
        //colliders.enabled = false;
        objectiveUI = FindObjectOfType<UpdateText>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // Don't play immediately
        audioSource.clip = portalOpenSFX; // Set the clip for the portal SFX
    }
    public void AddEnemy()
    {
        enemiesLeft += 1;
        Debug.Log("NumOfEnemiesnow " + enemiesLeft);
    }

    public void RemoveEnemy()
    {
        enemiesLeft -= 1;
        enemiesDefeated += 1;
        Debug.Log("Enemies killed " + enemiesDefeated);
        Debug.Log("Enemies to kill " + enemiesToKill);
        
        if(enemiesDefeated >= enemiesToKill)
        {
            //meshRenderer.enabled = true;
            //colliders.enabled = true;
            Debug.Log("Portal is active");

            BGMManager bgmManager = FindObjectOfType<BGMManager>();
            if (bgmManager != null)
            {
                Debug.Log("BGM deactivated");
                bgmManager.StopBGM();
            }

            if (portal != null) portal.SetActive(true);
            portalActive = true;

            PlayPortalSFX();
        }
        objectiveUI.ChangeText(enemiesDefeated, enemiesToKill);
    }
    
    public bool MaxEnemyCHecker()
    {   
        return !portalActive && enemiesLeft < maxEnemiesOnScreen;
    }

    public int KillObj()
    {
        return enemiesToKill;
    }

    public int DefeatedEnemies()
    {
        return enemiesDefeated;
    }

    private void PlayPortalSFX()
    {
        if (audioSource != null && portalOpenSFX != null)
        {
            audioSource.Play();
            Debug.Log("Portal open SFX played");
        }
        else
        {
            Debug.LogWarning("AudioSource or portalOpenSFX not assigned");
        }
    }
}
