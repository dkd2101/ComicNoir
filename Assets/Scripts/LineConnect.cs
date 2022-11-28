using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnect : MonoBehaviour
{
    public GameObject pointOne;
    public GameObject pointTwo;

    private List<GameObject> connectedObjects;

    private LineRenderer drawer;

    // Start is called before the first frame update
    void Start()
    {
        drawer = this.gameObject.AddComponent<LineRenderer>();
        
        drawer.SetWidth(0.05F, 0.05F);
        
        drawer.SetVertexCount(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
