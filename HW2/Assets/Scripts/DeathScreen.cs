using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public Button mainMenuButton;
    private GameObject GameManager;
    private GameObject Player;
    private SFXController playerSFX;
    private UpdateText objectiveUI;

    public AudioClip DeathSFX;
    private AudioSource audioSource;

    private void Start()
    {
        mainMenuButton.gameObject.SetActive(false); // Hide button initially
        Player = GameObject.Find("Death_Container/Hope_and_other_Delusion/Temp_char_for_load");
        playerSFX = GameObject.Find("Death_Container/Hope_and_other_Delusion").GetComponent<SFXController>();
        Player.SetActive(false);
        objectiveUI = FindObjectOfType<UpdateText>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; 
        audioSource.clip = DeathSFX;
    }

    public void ShowDeathScreen()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spawner in spawners)
        {
            Destroy(spawner);
        }

        BGMManager bgmManager = FindObjectOfType<BGMManager>();
        if (bgmManager != null){
            Debug.Log("BGM deactivated");
            bgmManager.StopBGM();
        }

        PlayDeathSFX();
        Player.SetActive(true);
        playerSFX.PlayDeath();
        objectiveUI.DisableText();
        StartCoroutine(PlayDeathSequence());
    }

    private IEnumerator PlayDeathSequence()
    {
        GameObject camera = GameObject.Find("PointTO");
        if (camera != null)
        {
            camera.GetComponent<Camera>().depth = 1;
        }
        else Debug.Log("No Alternative Camera");

        // Wait a bit after fading
        yield return new WaitForSeconds(2f);

        // Show the main menu button
        mainMenuButton.gameObject.SetActive(true);
    }

    public void LoadMainMenu()
    {   
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            Destroy(gameManager);
        }
        SceneManager.LoadScene("Homescreen");
    }

    private void PlayDeathSFX()
    {
        if (audioSource != null && DeathSFX != null)
        {
            audioSource.Play();
            Debug.Log("Portal open SFX played");
        }
        else
        {
            Debug.LogWarning("AudioSource or DeathSFX not assigned");
        }
    }
}
