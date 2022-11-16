using System;
using System.Collections;
using System.Collections.Generic;
using Interactions;
using UnityEngine;

public class ComicLayoutManager : MonoBehaviour
{
    private InteractionEventChannel _evtChannel;

    [SerializeField] private ComicLayout _comicLayout;

    public static ComicLayoutManager Instance;
    
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);

        Instance = this;
    }

    private void Start()
    {
        _evtChannel = InteractionEventChannel.Instance;
        _evtChannel.Subscribe(ClosePanel);
    }

    public void BeginNewStrip(Vector3 worldPos, ComicStrip strip)
    {
        if (_comicLayout.gameObject.activeSelf) return;

        transform.position = new Vector3(worldPos.x, worldPos.y);
        _comicLayout.Initialize(strip);
        _comicLayout.gameObject.SetActive(true);
    }

    private void ClosePanel(InteractionEvents evt)
    {
        if (evt != InteractionEvents.EndInteraction) return;

        _comicLayout.Clear();
        _comicLayout.gameObject.SetActive(false);
    }
}
