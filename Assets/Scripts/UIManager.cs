using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    [SerializeField]GameObject settingButton;
    [SerializeField] TextMeshProUGUI StageText;
    [SerializeField] TextMeshProUGUI MonsterText;

    public void OpenSetting()
    {
        GameManager.Instance.OnclickSettingButton();
    }

    private void Update()
    {
        StageText.text = "Stage : "+PlaneSceneManager.Instance.thisStage.ToString();
        if(PlaneSceneManager.Instance.bosscount > 0 )
        {
            MonsterText.text = "Boss Alive";
        }
        else
        {
            MonsterText.text="Monster : "+PlaneSceneManager.Instance.Monstercount.ToString();
        }
    }

}
