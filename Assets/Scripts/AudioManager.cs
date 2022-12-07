using System;
using System.Collections;
using System.Collections.Generic;
using Interactions;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource ambientSource;

    [SerializeField] private AudioClip doorSfx, discovery, breakthrough, thud;

    private void Start()
    {
        InteractionEventChannel.Instance.Subscribe(OnInteractionEvent);
    }

    private void OnInteractionEvent(InteractionEvents evt)
    {
        AudioClip clip;
        switch (evt)
        {
            case InteractionEvents.Body:
                clip = breakthrough;
                break;
            
            case InteractionEvents.BowlingBall:
            case InteractionEvents.GlassShard:
            case InteractionEvents.BankStatement:
                clip = discovery;
                break;
            
            case InteractionEvents.ToCrimeScene:
            case InteractionEvents.ToEvidence:
                clip = doorSfx;
                break;
            
            case InteractionEvents.OnEnterCrimeScene:
                clip = thud;
                break;
                
            case InteractionEvents.BrokenGlassDoor:
            case InteractionEvents.BowlingBallStand:
            case InteractionEvents.Book:
            case InteractionEvents.Diary:
            case InteractionEvents.FriendsPhoto:
            case InteractionEvents.Test:
            case InteractionEvents.None:
            case InteractionEvents.NextStep:
            case InteractionEvents.EndInteraction:
            default:
                return;
        }
        
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
