using UnityEngine;

public class EmissionIntencityChanger : MonoBehaviour
{
    [SerializeField] private float _min = 0f;
    [SerializeField] private float _max = 3f;
    private string EMISSION_PROPERTY_KEY = "_EmissionColor";

    private Renderer _targerRenderer;
    private void Awake()
    {
        _targerRenderer = GetComponent<Renderer>();
    }

    public void SetEmissionColor(float ratio)
    {
        float lerpedIntencity = Mathf.Lerp(_min, _max, ratio);
        _targerRenderer.material.SetColor(EMISSION_PROPERTY_KEY, _targerRenderer.material.color * lerpedIntencity);
    }
}
