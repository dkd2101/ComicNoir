using System;
using System.Linq;
using UnityEngine;

namespace Interactions
{
    public sealed class InteractionTrigger : MonoBehaviour
    {
        private InteractionEventChannel _evtChannel;
        private bool _canInteract;
        private bool _playerInTrigger;

        private bool _hasInteracted;

        public InteractionEvents triggerType;
        public bool multiInteract;

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
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1")) _evtChannel.Emit(InteractionEvents.EndInteraction);
            
            if (!(_canInteract && _playerInTrigger && Input.GetButtonDown("Interact"))) return;
            if (!multiInteract && _hasInteracted) return;
            
            _canInteract = false;
            _hasInteracted = true;
            _evtChannel.Emit(triggerType);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"collision {other.name}");
            if (!other.CompareTag("Player")) return;

            _playerInTrigger = true;
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log($"end collision {other.name}");
            if (!other.CompareTag("Player")) return;

            _playerInTrigger = false;
        }

        private void ResetInteractable(InteractionEvents intEvent)
        {
            if (intEvent == InteractionEvents.EndInteraction) _canInteract = true;
        }
    }
}