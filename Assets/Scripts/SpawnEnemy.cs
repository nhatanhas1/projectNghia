using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public List<GameObject> enemyPrefab;
    public List<Transform> spwanPosition;
    [SerializeField] float spawnTime;
    [SerializeField] float spawnDelay;
    int maxSpawn;
    int spawnCount;
    // Start is called before the first frame update
    void Start()
    {
        spawnDelay = 1.5f;
        maxSpawn = 20;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemyObject();
    }

    void SpawnEnemyObject()
    {
        spawnCount = 0;
        if(enemyPrefab != null && spwanPosition !=null)
        {
            if(Time.time > spawnTime)
            {
                spawnTime = Time.time + spawnDelay;
                if (spawnCount < maxSpawn)
                {
                    spawnCount++;
                    int tmp = Random.Range(0, enemyPrefab.Count);
                    int randomSpawnPotion = Random.Range(0, spwanPosition.Count);
                    Instantiate(enemyPrefab[tmp], spwanPosition[randomSpawnPotion].position, Quaternion.Euler(90, 0, 0));
                }
            }
                    
        }
    }
}
