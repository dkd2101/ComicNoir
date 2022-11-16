﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Interactions
{
    public sealed class InteractionTrigger : MonoBehaviour
    {
        private InteractionEventChannel _evtChannel;
        private bool _playerInTrigger;

        private bool _hasInteracted;

        public InteractionEvents triggerType;
        public bool canInteract;
        public bool multiInteract;

        public List<InteractionEvents> Prereqs = new List<InteractionEvents>();
        private Dictionary<InteractionEvents, bool> _fulfilledPrereqs = new Dictionary<InteractionEvents, bool>();

#if UNITY_EDITOR
        [SerializeField] private bool debug;
#endif

        public bool HasPrereqs => Prereqs.Count > 0;
        private bool MetPrereq(KeyValuePair<InteractionEvents, bool> prereq) => prereq.Value;
        public bool MeetsPrereqs => !HasPrereqs || (HasPrereqs && _fulfilledPrereqs.All(MetPrereq));

        public bool CanInteract => canInteract && (multiInteract || !_hasInteracted) && MeetsPrereqs;
        
#if UNITY_EDITOR
        private void Awake()
        {
            var colliders = GetComponents<Collider2D>();

            if (colliders.Any(col => col.isTrigger)) return;

            throw new Exception($"InteractionTrigger '{name}' does not have a Collider2D of type 'Trigger'");
        }
#endif
        
        private void Start()
        {
            _evtChannel = InteractionEventChannel.Instance;
            _evtChannel.Subscribe(ResetInteractable);

            foreach (var prereq in Prereqs)
            {
                _fulfilledPrereqs[prereq] = false;
            }
            canInteract = MeetsPrereqs;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1")) _evtChannel.Emit(InteractionEvents.EndInteraction);
            
            if (!(canInteract && _playerInTrigger && Input.GetButtonDown("Interact"))) return;
            if (!multiInteract && _hasInteracted) return;
            
            canInteract = false;
            _hasInteracted = true;
            _evtChannel.Emit(triggerType);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
#if UNITY_EDITOR
            if (debug) Debug.Log($"collision {other.name}");
#endif
            if (!other.CompareTag("Player")) return;

            _playerInTrigger = true;
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
#if UNITY_EDITOR
            Debug.Log($"end collision {other.name}");
#endif
            if (!other.CompareTag("Player")) return;

            _playerInTrigger = false;
        }

        private void ResetInteractable(InteractionEvents intEvent)
        {
            if (_fulfilledPrereqs.ContainsKey(intEvent)) _fulfilledPrereqs[intEvent] = true;
            if (intEvent == InteractionEvents.EndInteraction && MeetsPrereqs) canInteract = true;
        }
    }
}