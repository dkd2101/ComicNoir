using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragDrop : MonoBehaviour
{
    private bool isDrag = true;
    public UnityEvent onClick;
    [SerializeField] private LineConnect lc;

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
        if (isDrag)
        {
            if (flag)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                transform.Translate(mousePosition);
            }
        } else
        {
            //code for connecting
            onClick.Invoke();
        }
    }

    public void setDrag()
    {
        isDrag = !isDrag;
        lc.setDrag(isDrag);
    }
}
