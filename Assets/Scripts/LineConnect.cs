using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineConnect : MonoBehaviour
{
    public Transform startingPoint;

    private LineRenderer drawer;

    public static UnityEvent<Transform> selectItem;

    [SerializeField] private LineRenderer connectionPreview;

    [SerializeField] private LineRendererController _connectionPrefab;
    
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
        
        drawer = this.gameObject.AddComponent<LineRenderer>();
        
        drawer.SetWidth(0.05F, 0.05F);
        
        drawer.SetVertexCount(2);
    }

    private void Update()
    {
        if (startingPoint == null) return;
        
        connectionPreview.SetPosition(1, MousePosition);
    }

    private void SelectItem(Transform item)
    {
        if (startingPoint == null)
        {
            startingPoint = item;
            connectionPreview.SetPosition(0, startingPoint.position);
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
    }

    private void MakeConnection(Transform item1, Transform item2)
    {
        // Connect the evidence pieces, create a new LineRenderer and hand off to one of the evidence pieces
        // either Have those evidence pieces update the positions, or create
        var lrController = Instantiate(_connectionPrefab, transform);
        lrController.Initialize(item1, item2);
    }
    
}
