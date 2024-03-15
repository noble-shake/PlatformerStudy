using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_bu : MonoBehaviour
{
    [Header("Enemy Spawner")]
    [SerializeField] bool isSpawn = false;
    [SerializeField] List<GameObject> listEnemy;

    [SerializeField, Range(0.1f, 2.0f)] float spawnTime = 1.0f;
    float sTimer = 0.0f; // timer


    void Start()
    {
        
    }

    void Update()
    {
        checkSpawn();   
    }

    /// <summary>
    /// EnemySpawn Spawnable Check
    /// </summary>
    private void checkSpawn() {
        if (!isSpawn) return;

        sTimer += Time.deltaTime;
        if (sTimer > spawnTime) { 
            sTimer = 0.0f;
            // Spawn
            spawnEnemy();
        }

    }

    private void spawnEnemy() { 
        Random.Range(0, listEnemy.Count); // 0 ~ count - 1..
        //Random.Range(0.0f, 100.0f); // 0.0 ~ 100.0..
    }
}
