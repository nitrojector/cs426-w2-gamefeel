using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;

    [SerializeField] Vector3 spawnPosCenter;
    
    [SerializeField] float spawnPosRangeX = 8.0f;

    [SerializeField] private float spawnPosRangeY = 10.0f;
    
    Coroutine spawnRoutine;
    
    void Start()
    {
        spawnRoutine = StartCoroutine(SpawnBall());
    }

    void Update()
    {
    }

    IEnumerator SpawnBall()
    {
        yield return new WaitForSeconds(2.0f);
        while (true)
        {
            float randomX = Random.Range(-spawnPosRangeX, spawnPosRangeX);
            float randomY = Random.Range(0, spawnPosRangeY);
            Vector3 spawnPos = spawnPosCenter + new Vector3(randomX, randomY, 0);
            Instantiate(ballPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(6.0f);
        }
    }
}
