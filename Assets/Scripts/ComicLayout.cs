using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComicLayout : MonoBehaviour
{
    [SerializeField, Range(-1f, 1f)] private float spacing = 0.25f;
    
    private void OnValidate()
    {
        var children = new List<RectTransform>(GetComponentsInChildren<RectTransform>());
        children.RemoveAt(0);
        Debug.Log($"Respacing {children.Count()} children");

        float childrenHeight = children.Aggregate(0f, (soFar, child) => soFar + child.rect.height);
        float totalHeight = childrenHeight + spacing * (children.Count() - 1);
        float halfTotal = totalHeight * 0.5f;

        float soFar = 0;
        foreach (var child in children)
        {
            // Debug.Log(child.gameObject.name);
            var rect = child.rect;
            var y = halfTotal - soFar - rect.height * 0.5f;
            // Debug.Log(y);
            child.anchoredPosition = new Vector2(child.anchoredPosition.x, y);
            // Debug.Log(child.rect);
            soFar += rect.height + spacing;
        }
    }
    
}
