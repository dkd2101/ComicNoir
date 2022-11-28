using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class EvidenceSO : ScriptableObject
{
    [SerializeField]
    Sprite object_sprite;

    [SerializeField]
    string evidence_name;

    [SerializeField]
    string description;

    //this represented the current "connections" the player has made to this evidence
    // may need a add function for connected_evidence
    [SerializeField]
    public List<EvidenceSO> connected_evidence;

    [SerializeField]
    public List<Conditions> conclusions;
}
