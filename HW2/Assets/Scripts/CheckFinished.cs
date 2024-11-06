using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFinished : MonoBehaviour
{
    private int enemiesLeft = 0;
    private GameObject portal;
    private MeshRenderer meshRenderer;
    private Collider colliders;
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
    }
    public void AddEnemy()
    {
        enemiesLeft += 1;
    }

    public void RemoveEnemy()
    {
        enemiesLeft -= 1;
        if(enemiesLeft <= 0)
        {
            //meshRenderer.enabled = true;
            //colliders.enabled = true;
            if (portal != null) portal.SetActive(true);
        }
    }


}
