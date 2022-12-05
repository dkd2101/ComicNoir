using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineConnect : MonoBehaviour
{
    public Transform startingPoint;

    private LineRenderer drawer;

    private bool dragMode;

    public static readonly UnityEvent<Transform> selectItem = new UnityEvent<Transform>();

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
        
        drawer = this.gameObject.GetComponent<LineRenderer>();
        
        drawer.SetWidth(0.05F, 0.05F);
        
        drawer.SetVertexCount(2);

        this.startingPoint = null;
    }

    public void OnMouseDown()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        
        //SelectItem();
    }

    void Update()
    {
        if (startingPoint == null) return;
        if (!dragMode)
        {
            connectionPreview.SetPosition(1, MousePosition);
        } 
    }

    public void setDrag(bool drag)
    {
        this.dragMode = drag;
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
