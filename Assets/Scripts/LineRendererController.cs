using System;
using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    private Transform _evidence1, _evidence2;

    [SerializeField] private LineRenderer _line;

    private bool _initialized;

    private void Update()
    {
        if (!_initialized) return;
        
        SetPositions();
    }

    public void Initialize(Transform evidence1, Transform evidence2)
    {
        _evidence1 = evidence1;
        _evidence2 = evidence2;
        
        SetPositions();
        _line.enabled = true;
        
        _initialized = true;
    }

    void SetPositions()
    {
        _line.SetPosition(0, _evidence1.position);
        _line.SetPosition(1, _evidence2.position);
    }
}