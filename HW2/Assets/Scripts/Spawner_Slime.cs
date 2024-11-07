using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Slime : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float Interval = 5f;
    public int maxSpawn = 3;

    private int enemySpawned = 0;

    private CheckFinished checkFinished;

    void Start()
    {
        checkFinished = GameObject.FindObjectOfType<CheckFinished>();
        if (checkFinished == null){
            Debug.LogError("CheckFinished script not found in the scene.");
            return;
        }
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {   
            if (enemySpawned >= maxSpawn) Debug.Log ("Max enemy spawned reached");
            if (checkFinished != null && checkFinished.MaxEnemyCHecker() && enemySpawned < maxSpawn){
                enemySpawned += 1;
                Instantiate(enemyPrefab, transform.position, transform.rotation);
            }
        yield return new WaitForSeconds(Interval);
        }
    }
}
