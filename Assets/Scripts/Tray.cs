using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tray : MonoBehaviour
{
    [SerializeField] private float TraySpeed = 20f;

    [SerializeField] private float SideForceIntensity = 8 * 50.0f;

    [SerializeField] private float pitchRandomRange = 0.05f;

    private AudioSource _audioSource;

    private float XBound
    {
        get { return 20.0f - transform.localScale.x / 2; }
    }

    InputAction moveAction;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector2 move = moveAction.ReadValue<Vector2>();
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x + TraySpeed * Time.deltaTime * move.x, -XBound, XBound);
        transform.position = pos;
    }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 otherPos = other.transform.position;
        float diff = (otherPos.x - transform.position.x) / (transform.localScale.x / 2);


        Vector3 force = new Vector3(SideForceIntensity * diff, 0, 0);

        Debug.Log("Tray collided with " + other.gameObject.name + "with force: " + force);

        other.rigidbody.AddForce(force);

        if (EffectDirector.Enables(EffectType.SFX))
        {
            float randomOffset = UnityEngine.Random.Range(-pitchRandomRange, pitchRandomRange);
            _audioSource.pitch = 2.0f + randomOffset;
            _audioSource.Play();
        }
    }
}