using System;
using System.Collections;
using System.Collections.Generic;
using Interactions;
using UnityEngine;

public class testInteractableIndicator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _interactIcon;
    private InteractionTrigger _trigger;

    [SerializeField] private Sprite questionIcon, spyglassIcon;

    private bool colliding, interacting;
    private float beginCollisionTime;

    private readonly Color clearWhite = new Color(1f, 1f, 1f, 0f);
    
    public void Start()
    {
        _trigger = GetComponentInParent<InteractionTrigger>();
        _interactIcon.enabled = false;
        InteractionEventChannel.Instance.Subscribe(OnInteractionEvent);
    }

    void Update()
    {
        if (!colliding) return;

        if (interacting)
        {
            _interactIcon.color = Color.Lerp(clearWhite, Color.white, 0.8f);
            return;
        }

        var omega = Time.time - beginCollisionTime;
        var t = (1f - Mathf.Cos(omega * 3f)) * 0.4f;
        
        _interactIcon.color = Color.Lerp(clearWhite, Color.white, t);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        colliding = true;
        beginCollisionTime = Time.time;
        
        _interactIcon.sprite = _trigger.HasInteracted ? spyglassIcon : questionIcon;
        _interactIcon.enabled = _trigger.CanInteract;
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        colliding = false;
        _interactIcon.enabled = false;
    }

    private void OnInteractionEvent(InteractionEvents evt)
    {
        if (evt == InteractionEvents.NextStep) return;
        if (evt == InteractionEvents.EndInteraction)
        {
            interacting = false;
            return;
        }

        interacting = true;
        _interactIcon.sprite = spyglassIcon;
    }
}
