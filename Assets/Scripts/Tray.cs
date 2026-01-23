using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tray : MonoBehaviour
{
    private const float TraySpeed = 20f;
    private const float XBound = 12.0f;
    
    InputAction moveAction;
    
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
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
        float diff = otherPos.x - transform.position.x;
        
        other.rigidbody.AddForce(new Vector3(50.0f * diff, 0, 0));
        
        Debug.Log("Tray collided with " + other.gameObject.name);
    }
}
