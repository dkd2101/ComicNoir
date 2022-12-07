using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidence : MonoBehaviour
{
    [SerializeField] private EvidenceSO _evidence;
    private Dictionary<Evidence, LineRendererController> _lrLookup = new Dictionary<Evidence, LineRendererController>();
    [SerializeField] private ComicLayoutManager _layoutManager;

    void Start()
    {
        if (!_evidence.connected_evidence.Contains(_evidence))
        {
            _evidence.connected_evidence.Add(_evidence);
        }

        _layoutManager = ComicLayoutManager.Instance;
    }

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

    public void meetConditions()
    {
        foreach (Conditions c in _evidence.conclusions)
        {
            if (matchingLists(c.necesssarry_connections))
            {
                _layoutManager.BeginNewStrip(transform.position, c.conclusions);
            }
        }
    }

    private bool matchingLists(List<EvidenceSO> other)
    {
        foreach(EvidenceSO e in other)
        {
            if (!_evidence.connected_evidence.Contains(e))
            {
                return false;
            }
        }
        foreach(EvidenceSO e in _evidence.connected_evidence)
        {
            if (!other.Contains(e))
            {
                return false;
            }
        }
        return true;
    }

    private void OnMouseUpAsButton()
    {
        LineConnect.selectItem.Invoke(this);
    }
}
