using UnityEngine;
using UnityEngine.Events;

public class GameCore : MonoBehaviour
{
    [SerializeField] private int _maxHP = 10;
    private int _currHP;
    private static GameCore _instance;

    public UnityEvent<string> OnHpChagnedEvent;
    public UnityEvent OnHitEvent;
    public UnityEvent OnPlayerDestroyEvent;

    public static GameCore Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindAnyObjectByType<GameCore>();
            }
            return _instance;
        }
    }
    private void OnEnable()
    {
        _currHP = _maxHP;
        Debug.Log($"CurrHP : {_currHP} / MaxHP : {_maxHP}");
        UpdateUI();
    }
    private void Awake()
    {
        _instance = this;
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    var mob = collision.collider.GetComponent<MobController>();
    //    if (mob != null)
    //    {
    //        OnHitEvent?.Invoke();
    //        DecreaseHP(1);
    //        mob.Destroy();
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        var mob = other.GetComponent<MobController>();
        if (mob != null)
        {
            Debug.Log($"OnTriggerEnter CurrHP : {_currHP}");
            OnHitEvent?.Invoke();
            DecreaseHP(1);
            mob.Destroy();
        }
    }

    private void DecreaseHP(int amount)
    {
        if (_currHP <= 0)
        {
            return;
        }

        _currHP -= amount;
        Debug.Log($"CurrHP : {_currHP}");
        if (_currHP <= 0)
        {
            _currHP = 0;
            OnPlayerDestroyEvent?.Invoke();
        }
        UpdateUI();
    }


    private void UpdateUI()
    {
        OnHpChagnedEvent?.Invoke($"HP: {_currHP}");
    }
}
