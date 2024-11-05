using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private GameObject GameManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");

        if (GameManager != null) GameManager.GetComponent<SceneLoader>().LoadingScreen = gameObject;
        else Debug.Log("LoadingScreen Unable to locate Game Manager");

        gameObject.SetActive(false);
    }
}
