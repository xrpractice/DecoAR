using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform canvas;
    
    private void OnEnable()
    {
        ResizeCanvas();
    }

    private void ResizeCanvas()
    {
        canvas.localScale = Vector3.one;
        canvas.sizeDelta = ImageTracker.Instance.CurrentImageSize;
    }
}
