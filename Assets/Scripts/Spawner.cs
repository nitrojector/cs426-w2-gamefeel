using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] Ball ballPrefab;

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
            var go = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
            go.SetColor(Random.ColorHSV(0.0f, 1.0f, 0.0f, 1.0f, 0.8f, 1.0f));
            yield return new WaitForSeconds(6.0f);
        }
    }

}
