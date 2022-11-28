using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidence : MonoBehaviour
{
    [SerializeField] private EvidenceSO evidence;


    // Update is called once per frame
    void Update()
    {
         
    }

    void connectEvidence(EvidenceSO evidence)
    {
        this.evidence.connected_evidence.Add(evidence);
        evidence.connected_evidence.Add(this.evidence);
    }

    void disconnectEvidence(EvidenceSO evidence)
    {
        this.evidence.connected_evidence.Remove(evidence);
        evidence.connected_evidence.Remove(this.evidence);
    }
}
