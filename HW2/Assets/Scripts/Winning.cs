using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Winning : MonoBehaviour
{
    public Button mainMenuButton;
    private GameObject GameManager;
    //private GameObject Player;
    //private SFXController playerSFX;

    private void Start()
    {
        mainMenuButton.gameObject.SetActive(false); // Hide button initially
        //Player = GameObject.Find("Death_Container/Hope_and_other_Delusion/Temp_char_for_load");
        //Player.SetActive(false);
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
}