using System.Collections;
using System.Collections.Generic;
using Interactions;
using UnityEngine;

public class TriggerOnStart : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        
        GetComponent<InteractionTrigger>().BeginInteract();
        // yield return new WaitForSecondsRealtime(0.25f);
        //
        // InteractionEventChannel.Instance.Emit(InteractionEvents.NextStep);
    }
}
