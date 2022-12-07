using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Interactions;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField] private Image _fadeToBlack;
    private InteractionEventChannel _evtChannel;

    private bool _transitioning;
    private float _transitionStartTime;
    private float _delay;

    private void Start()
    {
        _evtChannel = InteractionEventChannel.Instance;

        _evtChannel.Subscribe(OnInteractionEvent);
    }

    private void OnInteractionEvent(InteractionEvents evt)
    {
        int buildIndex = -1;
        switch (evt)
        {
            case InteractionEvents.ToCrimeScene:
                buildIndex = 1;
                break;
            case InteractionEvents.ToEvidence:
                buildIndex = 2;
                break;
            default:
                return;
        }

        StartCoroutine(loadNewScene(buildIndex, 1.5f));
    }

    private void Update()
    {
        if (!_transitioning) return;

        var time = Time.time - _transitionStartTime;
        var t = Mathf.Clamp01(time / _delay);
        _fadeToBlack.color = Color.Lerp(Color.clear, Color.black, t);
    }

    private IEnumerator loadNewScene(int buildIndex, float delay = 0f)
    {
        _transitioning = true;
        _transitionStartTime = Time.time;
        _delay = delay;
        yield return new WaitForSecondsRealtime(delay);
        
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
    }
}
