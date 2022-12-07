using UnityEngine;

namespace DefaultNamespace
{
    public interface IComicPanel
    {
        RectTransform GetRectTransform();
        float GetHeight();
        bool IsChained();

        void SetActive(bool active);
        bool IsActive();
    }
}