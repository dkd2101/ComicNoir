
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ComicPanel
{
    public ComicPanelType type;

    public Alignment alignment;
    public Sprite frame;
    public Sprite image;
    
    public ComicTextType textType;
    public string text;
    public bool chain;
}

public enum ComicPanelType
{
    Image,
    Text
}

public enum Alignment
{
    Left,
    Center,
    Right
}

public enum ImageFrame
{
    Rect,
    ShearedRect,
    Trapezoid
}

[Serializable]
public struct ComicText
{
    // TODO: add different bubble types later
    public ComicTextType type;
    public string text;
}

public enum ComicTextType
{
    Monologue,
    McDialogue, // Main Character Dialogue
    NpcDialogue // Non-Player Character Dialogue
}

/*
 TODO: add different bubble types later
public enum DialogueType
{
    Normal,
    Shocked,
    
}
*/