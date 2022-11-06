using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMovement : MonoBehaviour
{
    [SerializeField] private float speed = 500;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        _rb.velocity = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime * Vector2.right;
        // transform.position += Input.GetAxis("Horizontal") * speed * Time.deltaTime * Vector3.right;
    }
}
