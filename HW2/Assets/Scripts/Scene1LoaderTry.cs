using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene1LoaderTry : MonoBehaviour
{
    public int sceneNum = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider others)
    {
        if(others.tag == "Player")
        {
            SceneManager.LoadScene(sceneNum);
        }        
    }
}
