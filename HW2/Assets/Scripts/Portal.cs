using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private SceneLoader sceneloader;
    
    public int SceneNum = 2;

    private UpdateText objectiveUI;
    // Start is called before the first frame update
    void Start()
    {
        sceneloader = GameObject.Find("GameManager").GetComponent<SceneLoader>();
        objectiveUI = FindObjectOfType<UpdateText>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider others)
    {
        if (others.tag == "Player")
        {
            objectiveUI.DisableText();
            sceneloader.LoadScene1(SceneNum);
        }
    }
}
