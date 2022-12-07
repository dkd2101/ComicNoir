using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class ComicImage : MonoBehaviour, IComicPanel
{
    public Image Frame, Panel;
    public RectTransform GetRectTransform() => (RectTransform)transform;
    public float GetHeight() => GetRectTransform().rect.height;

    public bool IsChained() => false;

    public void SetActive(bool active) => gameObject.SetActive(active);
    public bool IsActive() => gameObject.activeSelf;
}
