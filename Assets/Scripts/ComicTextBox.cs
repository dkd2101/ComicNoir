using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComicTextBox : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    public string Text
    {
        get => _text.text;
        set => _text.text = value;
    }

    public RectTransform rectTransform => (RectTransform)transform;
    private RectTransform parent => (RectTransform)transform.parent;

    private ComicPanel _textPanel;
    public ComicPanel TextPanel
    {
        get => _textPanel;
        set
        {
            _textPanel = value;
            Text = _textPanel.text;

            var pos = rectTransform.anchoredPosition;
            var size = rectTransform.rect.size;
            var parentSize = parent.rect.size;
            
            var halfSize = size * 0.5f;
            var offset = size * 0.25f;
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
        }
    }

}
