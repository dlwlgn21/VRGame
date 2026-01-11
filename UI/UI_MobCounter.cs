using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class UI_MobCounter : MonoBehaviour
{
    private int _killCount;
    private int _spawnCount;
    private TextMeshProUGUI _textUI;
    public void OnSpawnMob()
    {
        ++_spawnCount;
        UpdateUI();
    }
    public void OnKilledMob()
    {
        ++_killCount;
        UpdateUI();
    }

    private void OnEnable()
    {
        _killCount = 0;
        _spawnCount = 0;
        UpdateUI();
    }
    private void Awake()
    {
        _textUI = GetComponent<TextMeshProUGUI>();
    }
    private void UpdateUI()
    {
        if (!enabled)
        {
            return;
        }

        _textUI.text = $"Kill/Alive/Spawn\n{_killCount}/{_spawnCount - _killCount}/{_spawnCount}";
    }
}
