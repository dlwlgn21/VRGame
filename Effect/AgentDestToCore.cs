using UnityEngine;
using UnityEngine.AI;

public class AgentDestToCore : MonoBehaviour
{
    private NavMeshAgent _targetAgent;

    private void Awake()
    {
        _targetAgent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination()
    {
        Debug.Assert(_targetAgent != null);
        _targetAgent.SetDestination(GameCore.Instance.transform.position);
    }
}
