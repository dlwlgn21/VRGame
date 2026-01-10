using System.Collections;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private bool _bIsPlayOnStart = true;
    [SerializeField] private float _mobSpawnStartFactor  = 1f;
    [SerializeField] private float _mobSpawnAdditiveFactor = 0.1f;
    [SerializeField] private float _mobSpawnDelayPerSpawnGroup  = 5f;

    private void Start()
    {
        if (_bIsPlayOnStart)
        {
            Play();
        }
    }

    public void Play()
    {
        StartCoroutine(StartGameLoop());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator StartGameLoop()
    {
        float factor = _mobSpawnStartFactor;
        var waitForSec = new WaitForSeconds(_mobSpawnDelayPerSpawnGroup);

        while (true)
        {
            yield return waitForSec;


            yield return StartCoroutine(SpawnMob_Co(factor));
            factor += _mobSpawnAdditiveFactor;
        }
    }

    private IEnumerator SpawnMob_Co(float factor)
    {
        int count = (int)Random.Range(factor, factor * 2f);

        for (int i = 0; i < count; ++i)
        {
            SpawnMob();
            if (Random.value < 0.2f)
            {
                yield return new WaitForSeconds(Random.Range(0.01f, 0.02f));
            }
        }
    }

    private void SpawnMob()
    {
        Instantiate(_prefab, transform.position, transform.rotation);
    }
}
