using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        this.flag = false;
    }

    public void OnMouseDown()
    {
        this.flag = true;
    }

    public void OnMouseUp()
    {
        this.flag = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }
}
