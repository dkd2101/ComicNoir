using System;
using System.Collections;
using System.Collections.Generic;
using Interactions;
using UnityEngine;

public class testMovement : MonoBehaviour
{
    [SerializeField] private float speed = 500;

    private Rigidbody2D _rb;

    private bool _canMove = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        InteractionEventChannel.Instance.Subscribe(SetInDialogue);
    }
    
    private void Update()
    {
        if (_canMove)
            _rb.velocity = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime * Vector2.right;
        else _rb.velocity = Vector2.Lerp(_rb.velocity, Vector2.zero, 0.08f);
        // transform.position += Input.GetAxis("Horizontal") * speed * Time.deltaTime * Vector3.right;
    }
    
    private void SetInDialogue(InteractionEvents intEvent)
    {
        _canMove = intEvent switch
        {
            InteractionEvents.EndInteraction => true,
            InteractionEvents.None => throw new ArgumentOutOfRangeException(nameof(intEvent), intEvent, null),
            _ => false
        };
    }
}
