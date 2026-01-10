using UnityEngine;

public class LinePositionChanger : MonoBehaviour
{
    [SerializeField] private int _idx;

    private LineRenderer _targetLineRenderer;

    private void Awake()
    {
        _targetLineRenderer = GetComponent<LineRenderer>();
    }

    public void ChangePosition(Vector3 worldPos)
    {
        if (_targetLineRenderer.useWorldSpace)
        {
            _targetLineRenderer.SetPosition(_idx, worldPos);
        }
        else
        {
            _targetLineRenderer.SetPosition(_idx, transform.InverseTransformPoint(worldPos));
        }
    }
}
