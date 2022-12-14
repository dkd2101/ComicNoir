using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interactions;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    Vector2 movement;
    
    // For mapping y position [-5, 5] to a nice player scale
    [SerializeField] private AnimationCurve _playerScale;
    private float _playerScaleOffset;
    private float _xSpeedOffset;

    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private List<Sprite> _walkCycle;
    [SerializeField] private float _frameTime = 0.25f;
    private float _lastFrameChange = 0;
    private int _animFrame;
    private Sprite Idle => _walkCycle.First();

    [SerializeField] private ParticleSystem _spideySense;
    
    private bool _canMove = true;

    private void Start()
    {
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
        
        if (movement.magnitude > 0.05f) AnimateCharacter();
        else if (_animFrame != 0)
        {
            _animFrame = 0;
            _sr.sprite = Idle;
        }

        ScaleSprite();
    }

    private void FixedUpdate()
    {
        movement.x *= _xSpeedOffset * _playerScale.Evaluate(WorldPosToUnit());
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.deltaTime));
    }

    private void SetCanMove(InteractionEvents evt)
    {
        _canMove = (evt == InteractionEvents.EndInteraction);
        if (_canMove)
        {
            _spideySense.Play();
            var emission = _spideySense.emission;
            emission.enabled = false;
        }
        else _spideySense.Pause();
    }

    private void ScaleSprite()
    {
        transform.localScale = _playerScale.Evaluate(WorldPosToUnit()) *
                               _playerScaleOffset *
                               Vector3.one;
    }

    private float WorldPosToUnit() => (transform.position.y + 5f) * 0.1f;

    private void AnimateCharacter()
    {
        if (Mathf.Abs(movement.x) > 0.05f)
        {
            var direction = Mathf.Sign(movement.x);
            _sr.flipX = direction < 0;
        }

        var delta = Time.time - _lastFrameChange;
        if (delta < _frameTime) return;

        _lastFrameChange = Time.time;
        _sr.sprite = _walkCycle[++_animFrame % _walkCycle.Count];
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("InteractionTrigger")) return;

        var trigger = col.GetComponent<InteractionTrigger>();
        
        var emission = _spideySense.emission;
        if (!trigger.CanInteract || trigger.HasInteracted)
        {
            return;
        }
        
        emission.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (!col.CompareTag("InteractionTrigger")) return;

        var trigger = col.GetComponent<InteractionTrigger>();
        
        var emission = _spideySense.emission;
        if (!trigger.CanInteract || trigger.HasInteracted)
        {
            return;
        }
        
        emission.enabled = true;
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag("InteractionTrigger")) return;

        var trigger = col.GetComponent<InteractionTrigger>();
        if (!trigger.CanInteract) return;
        
        var emission = _spideySense.emission;
        emission.enabled = false;
    }
}
