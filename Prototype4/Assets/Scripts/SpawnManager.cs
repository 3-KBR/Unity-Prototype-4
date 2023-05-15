using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    private float spawnRange = 9;
    private int enemyCount;
    private int enemiesNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnWaves(enemiesNum);
    }

    void SpawnWaves(int enemiesNum)
    {
        for(int i = 0; i<enemiesNum ; i++){
            Instantiate(enemyPrefab,GenerateSpawnPosition(),enemyPrefab.transform.rotation);
        }
        Instantiate(powerUpPrefab,GenerateSpawnPosition(),powerUpPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0){
            enemiesNum++;
            SpawnWaves(enemiesNum);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange,spawnRange);
        float spawnPosZ = Random.Range(-spawnRange,spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX,0,spawnPosZ);
        return randomPos;
    }
}
