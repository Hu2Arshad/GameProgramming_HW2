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

    private void Start()
    {
        mainMenuButton.gameObject.SetActive(false); // Hide button initially
        Player = GameObject.Find("Death_Container/Hope_and_other_Delusion/Temp_char_for_load");
        playerSFX = GameObject.Find("Death_Container/Hope_and_other_Delusion").GetComponent<SFXController>();
        Player.SetActive(false);
    }

    public void ShowDeathScreen()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spawner in spawners)
        {
            Destroy(spawner);
        }
        Player.SetActive(true);
        playerSFX.PlayDeath();
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
}
