using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComicTextBox : MonoBehaviour
{
    [SerializeField] private Sprite _monologueBG, _mcDialogueBG, _npcDialogueBG;
    
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _background;
    
    public string Text
    {
        get => _text.text;
        set => _text.text = value;
    }

    private ComicTextType _type;
    public ComicTextType Type
    {
        get => _type;
        set
        {
            _type = value;
            _background.sprite = _type switch
            {
                ComicTextType.McDialogue => _mcDialogueBG,
                ComicTextType.NpcDialogue => _npcDialogueBG,
                ComicTextType.Monologue or _ => _monologueBG,
            };
        }
    }

    public RectTransform rectTransform => (RectTransform)transform;
    
}
