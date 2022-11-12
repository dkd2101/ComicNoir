using System;
using System.Collections;
using System.Collections.Generic;
using Interactions;
using UnityEngine;

public class testInteractableIndicator : MonoBehaviour
{
    private SpriteRenderer _sr;
    private InteractionTrigger _trigger;
    
    public void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _trigger = GetComponentInParent<InteractionTrigger>();
    }

    void Update()
    {
        _sr.color = _trigger.CanInteract ? Color.green : Color.white;
    }
}
