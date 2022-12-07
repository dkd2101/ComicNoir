using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComicTextBox : MonoBehaviour, IComicPanel
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private LayoutElement _layout;
    [SerializeField] private VerticalLayoutGroup _layoutGroup;
    
    public string Text
    {
        get => _text.text;
        set => _text.text = value;
    }

    public bool Chained { get; set; }
    
    public RectTransform rectTransform => (RectTransform)transform;
    public Rect rect => rectTransform.rect; //((RectTransform)_layout.transform).rect;
    private RectTransform parent => (RectTransform)transform.parent;

    private ComicPanel _textPanel;
    public ComicPanel TextPanel
    {
        get => _textPanel;
        set
        {
            _textPanel = value;
            Text = _textPanel.text;

            var parentSize = parent.rect.size;
            
            var halfSize = rect.center * 0.01f;
            var offset = halfSize * 0.5f;
            if (_textPanel.chain)
            {
                rectTransform.anchoredPosition = _textPanel.textType switch
                {
                    ComicTextType.McDialogue => new Vector2(parentSize.x * 0.5f + offset.x, 0),
                    ComicTextType.NpcDialogue => new Vector2(-parentSize.x * 0.5f - offset.x, 0),
                    ComicTextType.Monologue or _ => new Vector2(0, -parentSize.y * 0.25f - offset.y),
                };
            }
            else
            {
                rectTransform.anchoredPosition = _textPanel.textType switch
                {
                    ComicTextType.McDialogue => new Vector2(-parentSize.x * 0.5f + halfSize.x, 0),
                    ComicTextType.NpcDialogue => new Vector2(parentSize.x * 0.5f - halfSize.x, 0),
                    ComicTextType.Monologue or _ => rectTransform.anchoredPosition,
                };
            }

            SetPreferredHeight();
        }
    }

    private void OnValidate() => SetPreferredHeight();

    private void OnGUI() => SetPreferredHeight();

    private void SetPreferredHeight()
    {
        _layout.preferredHeight = _text.preferredHeight + _layoutGroup.padding.vertical;
    }

    public RectTransform GetRectTransform() => rectTransform;

    public float GetHeight() => rect.height * 0.01f;

    public bool IsChained() => Chained;

    public void SetActive(bool active)
    {
        SetPreferredHeight();
        gameObject.SetActive(active);
    }
    public bool IsActive() => gameObject.activeSelf;
}
