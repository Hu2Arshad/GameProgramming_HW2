using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject LoadingScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene1() // Load the first scene
    {
        if (LoadingScreen != null) LoadingScreen.SetActive(true); //Loading Screen
        else Debug.Log("Error Activating Loading Screen");

        SceneManager.LoadScene(1);
    }

}
