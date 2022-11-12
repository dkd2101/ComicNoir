using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComicStrip", menuName = "Comic/Comic Strip", order = 0)]
public class ComicStrip : ScriptableObject
{
    public List<ComicPanel> panels;
}
