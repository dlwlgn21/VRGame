using UnityEngine;
using UnityEngine.Events;

public class ColorRandomizer : MonoBehaviour
{
    public UnityEvent<Color> OnCreatedEvent;
    [Header("Color")]
    [SerializeField] private float _hueMin = 0f;
    [SerializeField] private float _hueMax = 1f;
    [SerializeField] private float _saturationMin = 0.7f;
    [SerializeField] private float _saturationMax = 1f;
    [SerializeField] private float _valueMin = 0.7f;
    [SerializeField] private float _valueMax = 1f;

    public void SetRandomColor()
    {
        OnCreatedEvent?.Invoke(Random.ColorHSV(_hueMin, _hueMax, _saturationMin, _saturationMax, _valueMin, _valueMax));
    }
}
