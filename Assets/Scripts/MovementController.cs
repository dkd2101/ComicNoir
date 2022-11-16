using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    Vector2 movement;
    private Animator playerAnim;
    
    // For mapping y position [-5, 5] to a nice player scale
    [SerializeField] private AnimationCurve _playerScale;
    private float _playerScaleOffset;

    private void Start()
    {
        this.playerAnim = this.GetComponent<Animator>();
        this.rb = this.GetComponent<Rigidbody2D>();
        
        // Get initial scale for relative scaling
        _playerScaleOffset = transform.localScale.y / _playerScale.Evaluate(WorldPosToUnit());
    }

    private void Update()
    {
        movement.y = Input.GetAxis("Vertical") * 0.5f;
        movement.x = Input.GetAxis("Horizontal");
        if (movement.magnitude > 1f) movement.Normalize();

        ScaleSprite();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    private void ScaleSprite()
    {
        transform.localScale = _playerScale.Evaluate(WorldPosToUnit()) *
                               _playerScaleOffset *
                               Vector3.one;
    }

    private float WorldPosToUnit() => (transform.position.y + 5f) * 0.1f;
}
