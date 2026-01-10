using UnityEngine;
using UnityEngine.AI;
public class AgentSpeedRatioRandomizer : MonoBehaviour
{
    [SerializeField] private float _min = 0.8f;
    [SerializeField] private float _max = 1.5f;

    private NavMeshAgent _targetAgent;

    private void Awake()
    {
        _targetAgent = GetComponent<NavMeshAgent>();
    }

    public void SetSpeed()
    {
        Debug.Assert(_targetAgent != null);
        _targetAgent.speed *= Random.Range(_min, _max);
    }
}
