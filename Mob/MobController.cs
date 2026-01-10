using UnityEngine;
using UnityEngine.Events;
public class MobController : MonoBehaviour
{
    public UnityEvent OnCreatedEvent; 
    public UnityEvent OnDestroyEvent; 

    [SerializeField] private float _destroyDelayTimeInSec = 1f;
    private bool _bIsDestroyed = false;

    public void Destroy()
    {
        if (_bIsDestroyed)
        {
            return;
        }
        MobManager.Instance.OnDestroyMob(this);
        _bIsDestroyed = true;
        Destroy(gameObject, _destroyDelayTimeInSec);
        OnDestroyEvent?.Invoke();
    }

    private void Start()
    {
        OnCreatedEvent?.Invoke();
        MobManager.Instance.OnSpawnedMob(this);
    }
}
