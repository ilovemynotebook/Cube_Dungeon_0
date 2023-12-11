using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBoss : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;

    [SerializeField] private Image _healthBar;

    [SerializeField] private Image _healthBar_AnimeVer;

    [SerializeField] private float _animeTotalDuration;

    private float _duration;

    private BossController _boss;



    private void Update()
    {
        AnimeHP();
    }


    public void Init(BossController boss, string bossName)
    {
        _boss = boss;
        _nameText.text = bossName;
        _boss.OnGetHitHandler += DecreaseHP;
    }


    private void DecreaseHP(float maxHP, float hp)
    {
        _healthBar.fillAmount = hp / maxHP;
    }


    private void AnimeHP()
    {

        if(_healthBar.fillAmount != _healthBar_AnimeVer.fillAmount)
        {
            _duration += Time.deltaTime;
            float percent = _duration / _animeTotalDuration;
            percent = percent * percent * (3f - 2f * percent);

            _healthBar_AnimeVer.fillAmount = Mathf.Lerp(_healthBar_AnimeVer.fillAmount, _healthBar.fillAmount, percent);
        }

        else
        {
            _duration = 0;
        }



    }
}
