using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private const float DespawnY = -30.0f;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.y < DespawnY)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // TODO: The Ball Death Screenshake
    }
}
