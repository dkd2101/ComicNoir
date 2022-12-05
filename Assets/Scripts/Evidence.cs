using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidence : MonoBehaviour
{
    [SerializeField] private EvidenceSO _evidence;
    private Dictionary<Evidence, LineRendererController> _lrLookup = new Dictionary<Evidence, LineRendererController>();

    public bool IsConnectedTo(Evidence other)
    {
        return _lrLookup.ContainsKey(other);
    }
    
    public void connectEvidence(Evidence evidence, LineRendererController lr)
    {
        _lrLookup.Add(evidence, lr);
        evidence._lrLookup.Add(this, lr);
        
        _evidence.connected_evidence.Add(evidence._evidence);
        evidence._evidence.connected_evidence.Add(_evidence);
    }

    public void disconnectEvidence(Evidence evidence)
    {
        var lr = _lrLookup[evidence];
        Destroy(lr.gameObject);
        
        _lrLookup.Remove(evidence);
        evidence._lrLookup.Remove(this);
        
        _evidence.connected_evidence.Remove(evidence._evidence);
        evidence._evidence.connected_evidence.Remove(_evidence);
    }

    private void OnMouseUpAsButton()
    {
        LineConnect.selectItem.Invoke(this);
    }
}
