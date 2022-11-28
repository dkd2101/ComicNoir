using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComicStrip", menuName = "Comic/Comic Strip", order = 0)]
public class ComicStrip : ScriptableObject
{
    /// <summary>
    /// Is this a conversation between two people?
    /// </summary>
    public bool isDialogue;
    public List<ComicPanel> panels;
}
