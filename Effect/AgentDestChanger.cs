using UnityEngine;
using UnityEngine.AI;
public class AgentDestChanger : MonoBehaviour
{
    [SerializeField] private Vector3 _destination;

    private NavMeshAgent _targetAgent;

    private void Awake()
    {
        _targetAgent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination()
    {
        Debug.Assert(_targetAgent != null);
        _targetAgent.SetDestination(_destination);
    }

}
