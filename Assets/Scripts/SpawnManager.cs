using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] powerups;
    public GameObject enemy;
    int enemyCount = 0;
    int wave = 1;
        // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPowerups", 3f, 5f); // spawns random powerups at random position after every 5 seconds
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length; // checks how many enemies are in scene
        if(enemyCount==0 & wave<6)
        {
            SpawnEnemies(wave);  // if enemies are 0 spawn new wave of enemy
            wave++; // increase wave number
        }
    }
    void SpawnPowerups()
    {
        
        int powerIndex = Random.Range(0, powerups.Length); // index to choose random powerup
        Instantiate(powerups[powerIndex], new Vector3(Random.Range(-8, 8), 1, Random.Range(-8, 8)), powerups[powerIndex].transform.rotation);
    }
    void SpawnEnemies(int wave)
    {
        for (int i= 0;i < wave;i++)
        {
            Instantiate(enemy, new Vector3(Random.Range(-8, 8), 1, Random.Range(-8, 8)), enemy.transform.rotation); // spawning enemies wave number times
        }
     
    }

}
