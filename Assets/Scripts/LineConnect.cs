using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineConnect : MonoBehaviour
{
    public Evidence startingPoint;

    public static readonly UnityEvent<Evidence> selectItem = new UnityEvent<Evidence>();

    [SerializeField] private LineRenderer connectionPreview;

    [SerializeField] private LineRendererController _connectionPrefab;

    [SerializeField] private bool _createLineModeActive = false;
    
    private Vector3 MousePosition {
        get {
            var mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mPos.z = 0;
            return mPos;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        selectItem.AddListener(SelectItem);

        if (connectionPreview == null) connectionPreview = GetComponent<LineRenderer>();
        
        connectionPreview.positionCount = 2;
    }

    private void Update()
    {
        if (startingPoint == null) return;
        
        connectionPreview.SetPosition(1, MousePosition);
    }

    private void SelectItem(Evidence item)
    {
        if (!_createLineModeActive) return;
        
        if (startingPoint == null)
        {
            startingPoint = item;
            connectionPreview.SetPosition(0, startingPoint.transform.position);
            connectionPreview.SetPosition(1, MousePosition);
            connectionPreview.enabled = true;
            return;
        }

        if (startingPoint == item)
        {
            startingPoint = null;
            connectionPreview.enabled = false;
            return;
        }

        MakeConnection(startingPoint, item);
        
        startingPoint = null;
        connectionPreview.enabled = false;
    }

    private void MakeConnection(Evidence item1, Evidence item2)
    {
        // Connect the evidence pieces, create a new LineRenderer and hand off to one of the evidence pieces
        // either Have those evidence pieces update the positions, or create
        if (item1.IsConnectedTo(item2))
        {
            item1.disconnectEvidence(item2);
            return;
        }
        
        var lrController = Instantiate(_connectionPrefab, transform);
        lrController.Initialize(item1.transform, item2.transform);

        item1.connectEvidence(item2, lrController);

        item1.meetConditions();
        item2.meetConditions();
    }

    public void ToggleActive()
    {
        _createLineModeActive = !_createLineModeActive;
        if (!_createLineModeActive) ClearLine();
    }

    private void ClearLine()
    {
        startingPoint = null;
        connectionPreview.enabled = false;
    }

}
