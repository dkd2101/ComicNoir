using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interactions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComicLayout : MonoBehaviour
{
    private InteractionEventChannel _evtChannel;
    
    [SerializeField, Range(-1f, 1f)] private float spacing = 0.25f;

    private List<ComicPanel> _strip;
    private int index = -1;

    private List<RectTransform> _children;

    [SerializeField] private ComicImage panel;
    [SerializeField] private ComicTextBox textBox;

    private void Awake()
    {
        _evtChannel = InteractionEventChannel.Instance;
        _evtChannel.Subscribe(NextPanel);
    }

    private void Update()
    {
        if (index == -1) return;
        
        if (Input.GetButtonDown("Interact")) NextPanel(InteractionEvents.NextStep);
    }

    public void Initialize(ComicStrip strip)
    {
        _strip = new List<ComicPanel>(strip.panels);
        index = 0;

        _children = new List<RectTransform>();
        
        Debug.Log(strip);

        foreach (var stripPanel in _strip)
        {
            Debug.Log(stripPanel.type);
            switch (stripPanel.type)
            {
                case ComicPanelType.Image:
                    var i = Instantiate(panel, (RectTransform)transform);
                    var frame = i.Frame;
                    var frameRect = frame.rectTransform;
                    var image = i.Panel;
                    var imageRect = image.rectTransform;
                    
                    if (stripPanel.alignment == AlignImage.Right)
                    {
                        var anchor = new Vector2(1, 0.5f);
                        frameRect.anchorMin = frameRect.anchorMax = anchor;
                        frameRect.pivot = anchor;
                        frameRect.anchoredPosition = Vector2.zero;
                    }

                    image.sprite = stripPanel.image;
                    image.SetNativeSize();
                    image.preserveAspect = true;

                    var frameSize = frameRect.rect.size;
                    var imageSize = imageRect.rect.size;
                    var scale = Mathf.Max(frameSize.x / imageSize.x, frameSize.y / imageSize.y);
                    imageRect.transform.localScale = Vector3.one * scale;
                    
                    _children.Add(frameRect);
                    break;
                
                case ComicPanelType.Text:
                    var t = Instantiate(textBox, transform);
                    Debug.Log(t.name);
                    t.Text = stripPanel.text;
                    _children.Add(t.rectTransform);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _children[^1].gameObject.SetActive(false);
        }
        NextPanel(InteractionEvents.NextStep);
    }
    
    private void Respace()
    {
        var children = GetActiveChildren();
        
        Debug.Log($"Spacing {children.Count()} of {_children.Count} children.");
        
        float childrenHeight = children.Aggregate(0f, (soFar, child) => soFar + child.rect.height);
        float totalHeight = childrenHeight + spacing * (children.Count() - 1);
        float halfTotal = totalHeight * 0.5f;

        float soFar = 0;
        foreach (var child in children)
        {
            // Debug.Log(child.gameObject.name);
            var rect = child.rect;
            var y = halfTotal - soFar - rect.height * 0.5f;
            // Debug.Log(y);
            child.anchoredPosition = new Vector2(child.anchoredPosition.x, y);
            // Debug.Log(child.rect);
            soFar += rect.height + spacing;
        }
    }

    private void NextPanel(InteractionEvents evt)
    {
        if (evt != InteractionEvents.NextStep) return;
        if (index >= _children.Count)
        {
            _evtChannel.Emit(InteractionEvents.EndInteraction);
            return;
        }
        
        _children[index++].gameObject.SetActive(true);
        Respace();
    }

    private IEnumerable<RectTransform> GetActiveChildren() => _children.Where(child => child.gameObject.activeSelf);

    public void Clear()
    {
        Debug.Log("clearing...");
        _strip.Clear();
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        _children.Clear();
        index = -1;
    }
}
