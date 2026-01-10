using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MobManager : MonoBehaviour
{
    private static MobManager _instance;


    public UnityEvent<MobController> OnSpawnEvent;
    public UnityEvent<MobController> OnDestroyEvent;

    private List<MobController> _mobs = new(512);
    public static MobManager Instance
    {
        get 
        {
            if (_instance == null)
            {
                _instance = GameObject.FindAnyObjectByType<MobManager>();
            }
            return _instance; 
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    public void OnSpawnedMob(MobController mob)
    {
        _mobs.Add(mob);
        OnSpawnEvent?.Invoke(mob);
    }
    public void OnDestroyMob(MobController mob)
    {
        if (_mobs.Remove(mob))
        {
            OnDestroyEvent?.Invoke(mob);
        }
    }

    public void DesroyAllMob()
    {
        while (_mobs.Count > 0)
        {
            if (_mobs[0] != null)
            {
                _mobs[0].Destroy();
            }
        }
        _mobs.Clear();
    }
}
