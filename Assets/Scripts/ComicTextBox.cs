using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComicTextBox : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public string Text
    {
        get => _text.text;
        set => _text.text = value;
    }

    public RectTransform rectTransform => (RectTransform)transform;
}
