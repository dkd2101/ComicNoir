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
        // transform.position += Input.GetAxis("Horizontal") * speed * Time.deltaTime * Vector3.right;
    }
    
    private void SetInDialogue(InteractionEvents intEvent)
    {
        switch (intEvent)
        {
            case InteractionEvents.EndInteraction:
                _canMove = true;
                break;
            case InteractionEvents.NextStep:
            case InteractionEvents.MurderWeapon:
                _canMove = false;
                break;
            case InteractionEvents.None:
            default:
                throw new ArgumentOutOfRangeException(nameof(intEvent), intEvent, null);
        }
    }
}
