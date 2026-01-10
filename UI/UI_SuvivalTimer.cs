using UnityEngine;
using TMPro;

public class UI_SuvivalTimer : MonoBehaviour
{
    private float _startTimeInSec;
    private TextMeshProUGUI _textUI;

    private void OnEnable()
    {
        _startTimeInSec = Time.time;
    }
    private void Awake()
    {
        _textUI = GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        _textUI.text = $"SuvivalTime\n{Time.time - _startTimeInSec:0.0}s";
    }
}
