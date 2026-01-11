using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class UI_MagazineChargeSlider : MonoBehaviour
{
    private Slider _slider;

    private void Awake()
    {
        EnsureComponent();
    }

    public void OnChangedChargingValue(float value)
    {
        EnsureComponent();
        _slider.value = value;
    }

    private void EnsureComponent()
    {
        if (_slider == null)
        {
            _slider = GetComponent<Slider>();
        }
    }
}
