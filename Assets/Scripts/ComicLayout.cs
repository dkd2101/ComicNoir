using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using DefaultNamespace;
using Interactions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ComicLayout : MonoBehaviour
{
    private const float NoDialogueScale = 0.6f;
    
    private InteractionEventChannel _evtChannel;
    
    [SerializeField, Range(-1f, 1f)] private float spacing = 0.25f;

    private RectTransform _rectTransform;
    private Rect _layoutRect;

    private List<ComicPanel> _strip;
    private int index = -1;

    private List<IComicPanel> _children;
    private RectTransform leftImage, rightImage;
    private Alignment monologueAlignment;

    [SerializeField] private ComicImage panel;
    [SerializeField] private ComicTextBox monologueBox, mcDialogueBox, npcDialogueBox;

    private void Awake()
    {
        _evtChannel = InteractionEventChannel.Instance;
        _evtChannel.Subscribe(NextPanel);

        _rectTransform = (RectTransform)transform;
        _layoutRect = _rectTransform.rect;
    }

    private void Update()
    {
        if (index == -1) return;
        
        if (Input.GetButtonDown("Interact")) NextPanel(InteractionEvents.NextStep);
    }

    private void LateUpdate()
    {
        if (Input.GetButton("Interact")) Respace();
    }

    private void OnValidate()
    {
        Respace();
    }

    public void Initialize(ComicStrip strip)
    {
        _strip = new List<ComicPanel>(strip.panels);
        index = 0;
        monologueAlignment = Alignment.Center;

        _children = new List<IComicPanel>();
        
        Debug.Log(strip);
        
        _strip.ForEach(InitializePanel(strip.isDialogue));
        
        NextPanel(InteractionEvents.NextStep);
    }

    private Action<ComicPanel> InitializePanel(bool isDialogue) => stripPanel =>
        {
            Debug.Log(stripPanel.type);

            switch (stripPanel.type)
            {
                case ComicPanelType.Image:
                    var image = CreateImagePanel(stripPanel, isDialogue);
                    _children.Add(image);
                    break;

                case ComicPanelType.Text:
                    CreateTextPanel(stripPanel, isDialogue);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            _children[^1].SetActive(false);
        };

    private IComicPanel CreateImagePanel(ComicPanel imagePanel, bool isDialogue)
    {
        var i = Instantiate(panel, (RectTransform)transform);
        var frame = i.Frame;
        var frameRect = frame.rectTransform;
        var image = i.Panel;
        var imageRect = image.rectTransform;

        const float scaling = NoDialogueScale * 0.5f;
        var anchor = imagePanel.alignment switch {
            Alignment.Left => new Vector2(isDialogue ? 0 : 0.5f - scaling, 0.5f),
            Alignment.Center => new Vector2(0.5f, 0.5f),
            Alignment.Right => new Vector2(isDialogue ? 1 : 0.5f + scaling, 0.5f),
            _ => throw new ArgumentOutOfRangeException()
        };
        frameRect.anchorMin = frameRect.anchorMax = anchor;
        frameRect.pivot = anchor;
        frameRect.anchoredPosition = Vector2.zero;

        image.sprite = imagePanel.image;
        image.SetNativeSize();
        image.preserveAspect = true;

        var frameSize = frameRect.rect.size;
        var imageSize = imageRect.rect.size;
        var scale = Mathf.Max(frameSize.x / imageSize.x, frameSize.y / imageSize.y);
        imageRect.transform.localScale = Vector3.one * scale;
        
        return i;
    }

    private void CreateTextPanel(ComicPanel textPanel, bool isDialogue)
    {
        var textBox = textPanel.textType switch
        {
            ComicTextType.McDialogue => mcDialogueBox,
            ComicTextType.NpcDialogue => npcDialogueBox,
            ComicTextType.Monologue or _ => monologueBox
        };
        
        var text = Instantiate(textBox, transform);
        Debug.Log(text.name);
        text.Text = textPanel.text;
        text.Chained = textPanel.chain;

        var rectTransform = text.rectTransform;
        var layoutSize = new Vector2(((RectTransform)transform).rect.size.x, 0) * (isDialogue ? 0.5f : NoDialogueScale);
        
        var rect = text.rect;
        var size = rect.size * 0.01f;
        Debug.Log(size);
            
        var halfSize = size * 0.5f;
        var quarterSize = halfSize * 0.5f;
        
        rectTransform.anchoredPosition = textPanel.textType switch
        {
            ComicTextType.NpcDialogue => new Vector2(quarterSize.x, 0),
            ComicTextType.McDialogue or ComicTextType.Monologue or _ => new Vector2(-quarterSize.x, 0),
            // ComicTextType.Monologue or _ => new Vector2(0, -parentSize.y * 0.25f - offset.y),
        };

        if (textPanel.textType == ComicTextType.Monologue)
        {
            if (isDialogue)
            {
                rectTransform.anchoredPosition = Vector2.right * monologueAlignment switch
                {
                    Alignment.Left => -layoutSize.x + halfSize.x,
                    Alignment.Center => -layoutSize.x * 0.5f,
                    Alignment.Right => -halfSize.x,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            else
            {
                rectTransform.anchoredPosition = Vector2.right * monologueAlignment switch
                {
                    Alignment.Left => -layoutSize.x * 0.5f + halfSize.x,
                    Alignment.Center => 0,
                    Alignment.Right => layoutSize.x * 0.5f - halfSize.x,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            NextMonologueAlignment();
        }
        
        if (textPanel.chain) rectTransform.SetParent(_children[^1].GetRectTransform());
        
        _children.Add(text);
    }
    
    private void NextMonologueAlignment() => monologueAlignment = monologueAlignment switch {
        Alignment.Left or Alignment.Center => Alignment.Right,
        Alignment.Right => Alignment.Left,
        _ => throw new ArgumentOutOfRangeException()
    };
    
    private void Respace()
    {
        var children = GetActiveChildren();
        
        Debug.Log($"Spacing {children.Count()} of {_children.Count} children.");
        
        float childrenHeight = children.Aggregate(0f, (soFar, child) =>
        {
            //var scale = child.Item4 ? 0.01f : 1f;
            return soFar + child.GetHeight();
        });
        float totalHeight = childrenHeight + spacing * (children.Count() - 1);
        float halfTotal = totalHeight * 0.5f;

        float soFar = 0;
        foreach (var child in children)
        {
            // Debug.Log(child.gameObject.name);
            var rectTransform = child.GetRectTransform();
            var rectHeight = child.GetHeight();
            // if (child.Item4) rectHeight *= 0.01f;
            
            var y = halfTotal - soFar - rectHeight * 0.5f;
            // Debug.Log(y);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);
            // Debug.Log(child.rect);
            soFar += rectHeight + spacing;
        }

        for (int ii = 0; ii < index; ++ii) _children[ii].SetActive(true);
    }

    private void NextPanel(InteractionEvents evt)
    {
        if (evt != InteractionEvents.NextStep) return;
        if (index >= _children.Count)
        {
            _evtChannel.Emit(InteractionEvents.EndInteraction);
            return;
        }
        
        // _children[index].SetActive(true);
        if (!_children[index++].IsChained()) Respace();
        
    }

    private IEnumerable<IComicPanel> GetActiveChildren() => _children
        .Where((child, _index) => _index < index &&!child.IsChained());

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
