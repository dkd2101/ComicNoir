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


    void UpdateLines()
    {
        for(int i = 0; i < connectedObjects.Count; i++)
        {
            if(i+1 < connectedObjects.Count)
            {
                pointOne = connectedObjects[i];
                pointTwo = connectedObjects[i + 1];
                drawer.SetPosition(0, pointOne.transform.position);
                drawer.SetPosition(1, pointTwo.transform.position);
            }
        }
    }
}