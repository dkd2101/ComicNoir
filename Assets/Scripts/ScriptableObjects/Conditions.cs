using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Conditions : ScriptableObject
{
    [SerializeField]
    public List<EvidenceSO> necesssarry_connections;

    [SerializeField]
    public ComicStrip conclusions;
}
