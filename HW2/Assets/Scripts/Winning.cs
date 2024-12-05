using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Winning : MonoBehaviour
{
    public Button mainMenuButton;
    private GameObject GameManager;
    private GameObject PlayerInGame;
    //private GameObject Player;
    //private SFXController playerSFX;

    public AudioClip WinSFX;
    private AudioSource audioSource;

    private void Start()
    {
        mainMenuButton.gameObject.SetActive(false); // Hide button initially
        PlayerInGame = GameObject.Find("Player");
        //Player = GameObject.Find("Death_Container/Hope_and_other_Delusion/Temp_char_for_load");
        //Player.SetActive(false);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; 
        audioSource.clip = WinSFX;
    }

    void OnTriggerEnter(Collider others)
    {
        if (others.tag == "Player")
        {
            ShowWinScreen();
        }
    }

    public void ShowWinScreen()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spawner in spawners)
        {
            Destroy(spawner);
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        BGMManager bgmManager = FindObjectOfType<BGMManager>();
        if (bgmManager != null){
            Debug.Log("BGM deactivated");
            bgmManager.StopBGM();
        }

        PlayWinSFX();

        //Player.SetActive(true);
        StartCoroutine(PlayWinSequence());
    }

    private IEnumerator PlayWinSequence()
    {
        GameObject camera = GameObject.Find("WinCam");
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

    private void PlayWinSFX()
    {
        if (audioSource != null && WinSFX != null)
        {
            audioSource.Play();
            Debug.Log("Portal open SFX played");
        }
        else
        {
            Debug.LogWarning("AudioSource or WinSFX not assigned");
        }
    }
}
