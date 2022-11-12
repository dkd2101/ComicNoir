
using System.Collections.Generic;
using UnityEngine;

public struct ComicPanel
{
    public ComicPanelType Type;

    public ComicPanelImageFrame Frame;
    public Sprite Image;
    public List<ComicText> Texts; 
    
    public string Monologue;
}

public enum ComicPanelType
{
    Image,
    Monologue
}

public enum ComicPanelImageFrame
{
    Rect,
    ShearedRect,
    Trapezoid
}

public struct ComicText
{
    // TODO: add different bubble types later
    public ComicTextType Type;
    public string Text;
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