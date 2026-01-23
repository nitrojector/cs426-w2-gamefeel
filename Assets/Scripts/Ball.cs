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

    private void OnCollisionEnter(Collision other)
    {
        // Screen shake on collision
        Vector3 collisionNormal = other.contacts[0].normal;
        Vector2 shakeDir = new Vector2(collisionNormal.x, collisionNormal.y).normalized;
        float shakeRot = 2.0f;
        float shakeIntensity = 5.0f;
        float shakeDuration = 0.8f;

        if (other.gameObject.name.Contains("[S]"))
            VFXManager.Instance.ScreenShake(shakeDir, shakeRot, shakeIntensity, shakeDuration);
    }

    private void OnDestroy()
    {
        // TODO: The Ball Death Screenshake
    }
}
