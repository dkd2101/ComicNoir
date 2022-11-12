
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ComicPanel
{
    public ComicPanelType type;

    public AlignImage alignment;
    public ImageFrame frame;
    public Sprite image;
    public List<ComicText> texts; 
    
    public string monologue;
}

public enum ComicPanelType
{
    Image,
    Monologue
}

public enum AlignImage
{
    Left = -1,
    Center = 0,
    Right = 1
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