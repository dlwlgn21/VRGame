using UnityEngine;

public class EmissionColorChanger : MonoBehaviour
{
    [SerializeField] private float _emissionIntencity = 5f;
    private string EMISSION_PROPERTY_KEY = "_EmissionColor";

    private Renderer _targerRenderer;
    private void Awake()
    {
        _targerRenderer = GetComponent<Renderer>();
    }

    public void SetEmissionColor(Color color)
    {
        _targerRenderer.material.SetColor(EMISSION_PROPERTY_KEY, color * _emissionIntencity);
    }
}
