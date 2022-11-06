#define DEBUG_EVT_CHANNEL
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class InteractionEventChannel : MonoBehaviour
    {
        private readonly UnityEvent<InteractionEvents> _eventEmitter = new UnityEvent<InteractionEvents>();

        #region Singleton
        
        public static InteractionEventChannel Instance;
        
        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);

            Instance = this;
        }

        #endregion

        #region Lifecycle Methods
        
        private void OnDestroy() => _eventEmitter.RemoveAllListeners();

        #endregion

        public UnityAction Subscribe(UnityAction<InteractionEvents> toSubscribe)
        {
            _eventEmitter.AddListener(toSubscribe);
            return () => Unsubscribe(toSubscribe);
        }
        
        private void Unsubscribe(UnityAction<InteractionEvents> toUnsubscribe) => _eventEmitter.RemoveListener(toUnsubscribe);

#if DEBUG_EVT_CHANNEL
        public void Emit(InteractionEvents intEvent)
        {
            Debug.Log($"Emitting event: {intEvent}");
            _eventEmitter.Invoke(intEvent);
        }
#else
        public void Emit(InteractionEvents intEvent) => _eventEmitter.Invoke(intEvent);
#endif
    }
}