using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform canvas;

    private readonly Func<float, float> _meterToKm = (meter) => meter / 1000;
    private readonly Func<float, float> _kmToMeter = (km) => km * 1000;
    
    private void OnEnable()
    {
        ResizeCanvas();
    }

    private void ResizeCanvas()
    {
        var oneMeterInKm = _meterToKm(1);
        canvas.localScale = new Vector2(oneMeterInKm, oneMeterInKm);

        var height = ImageTracker.Instance.CurrentImageSize.x;
        var width = ImageTracker.Instance.CurrentImageSize.y;
        canvas.sizeDelta = new Vector2(_kmToMeter(height), _kmToMeter(width));
    }
}
