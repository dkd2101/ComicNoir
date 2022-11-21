using System.Collections;
using System.Collections.Generic;
using Interactions;
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
    private float _xSpeedOffset;

    private bool _canMove = true;

    private void Start()
    {
        this.playerAnim = this.GetComponent<Animator>();
        this.rb = this.GetComponent<Rigidbody2D>();

        InteractionEventChannel.Instance.Subscribe(SetCanMove);
        
        // Get initial scale for relative scaling
        var scale = _playerScale.Evaluate(WorldPosToUnit());
        _playerScaleOffset = transform.localScale.y / scale;
        _xSpeedOffset = moveSpeed / scale;
    }

    private void Update()
    {
        if (!_canMove)
        {
            movement = Vector2.zero;
            return;
        }
        
        movement.y = Input.GetAxis("Vertical") * 0.75f;
        movement.x = Input.GetAxis("Horizontal");
        if (movement.magnitude > 1f) movement.Normalize();

        ScaleSprite();
    }

    private void FixedUpdate()
    {
        movement.x *= _xSpeedOffset * _playerScale.Evaluate(WorldPosToUnit());
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    private void SetCanMove(InteractionEvents evt) => _canMove = (evt == InteractionEvents.EndInteraction);

    private void ScaleSprite()
    {
        transform.localScale = _playerScale.Evaluate(WorldPosToUnit()) *
                               _playerScaleOffset *
                               Vector3.one;
    }

    private float WorldPosToUnit() => (transform.position.y + 5f) * 0.1f;
}