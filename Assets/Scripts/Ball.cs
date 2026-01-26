using UnityEngine;

public class Ball : MonoBehaviour
{
    private const float DespawnY = -30.0f;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    private Material _material;
    private Rigidbody _rb;

    private float _size = 5.0f;

    private void Awake()
    {
        transform.localScale = Vector3.one * _size;
        _material = GetComponent<Renderer>().material;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.position.y < DespawnY)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (EffectDirector.Enables(EffectType.Expand))
        {
            // float delta = Vector3.Dot(_rb.GetAccumulatedForce(), _rb.linearVelocity.normalized);
            var delta = _rb.linearVelocity.y;
            if (delta < 0) delta = -delta;
            var range = 18.0f;
            var offset = 3.0f;
            var max = range - offset;
            var min = 0.0f - offset;

            var ls = Mathf.Lerp(0.8f * _size, 1.3f * _size, (delta - min) / range) * Vector3.one;
            transform.localScale = ls;
        }
    }

    private void OnDestroy()
    {
        // TODO: The Ball Death Screenshake
    }

    private void OnCollisionEnter(Collision other)
    {
        // Screen shake on collision
        var collisionNormal = other.contacts[0].normal;
        var shakeDir = new Vector2(collisionNormal.x, collisionNormal.y).normalized;
        var shakeRot = 2.0f;
        var shakeIntensity = 5.0f;
        var shakeDuration = 0.8f;

        if (other.gameObject.name.Contains("[S]"))
            VFXManager.Instance.ScreenShake(shakeDir, shakeRot, shakeIntensity, shakeDuration);

        var wallComp = other.gameObject.GetComponent<Wall>();
        if (wallComp != null) wallComp.AdjustColorWithBallColor(GetComponent<Renderer>().material.color);
    }

    public void SetSize(float size)
    {
        _size = size;
        transform.localScale = Vector3.one * _size;
    }

    public void SetColor(Color color)
    {
        _material.color = color;
        _material.SetColor(EmissionColor, color * 3.0f);
    }
}