using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThumbleweedBehavior : MonoBehaviour
{
    [SerializeField] private const float pushForce = 2.5f;
    private Rigidbody rb;
    bool hasForce = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move()
    {
        if (!hasForce)
        {
            rb.AddForce(Vector3.right * pushForce, ForceMode.Force);
        }
    }

    public void Reset()
    {
        rb.velocity.Set(0, 0, 0);
        hasForce = false;
        transform.position = new Vector3(-8.4f, 0.36f, -4.3f);
    }
}
