using TMPro;
using UnityEngine;
[RequireComponent(typeof(TMP_Text))]
public class UI_MagazineCounter : MonoBehaviour
{
    private TMP_Text _magazineText;
    private void Awake()
    {
        EnsureComponent();
    }
    public void OnBulletChanged(int currBullet, int maxBullet)
    {
        EnsureComponent();
        _magazineText.text = $"Magazine\n{currBullet}/{maxBullet}";
    }

    private void EnsureComponent()
    {
        if (_magazineText == null)
        {
            _magazineText = GetComponent<TMP_Text>();
        }
    }
}
