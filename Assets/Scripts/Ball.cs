using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    private const float DespawnY = -30.0f;
    
    private Material _material;
    
    void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (transform.position.y < DespawnY)
            Destroy(gameObject);
    }

    public void SetColor(Color color)
    {
        _material.color = color;
        _material.SetColor(EmissionColor, color * 3.0f);
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
        
        var wallComp = other.gameObject.GetComponent<Wall>();
        if (wallComp != null)
        {
            wallComp.AdjustColorWithBallColor(GetComponent<Renderer>().material.color);
            return;
        }
    }

    private void OnDestroy()
    {
        // TODO: The Ball Death Screenshake
    }
}
