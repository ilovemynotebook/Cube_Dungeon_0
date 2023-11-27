using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.ComponentModel;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject Cube;
    public GameObject SettingScreen;
    public CanvasGroup TitleScreen;
    public CanvasGroup Buttons;
    public Button TitleScreenButton;
    public Button SettingButton;
    public Button StartButton;
    public Button LoadButton;
    public float TitleFadeTime;
    public bool TitleFadeIsIn;
    void Awake()
    {
        TitleScreen.gameObject.SetActive(false);
        
    }
    private void OnEnable()
    {
        SettingButton.onClick.AddListener(() => OnButtonGameObject(true, SettingScreen));
        StartButton.onClick.AddListener(() => OnButtonStartGame());
        LoadButton.onClick.AddListener(() => OnButtonLoadGame());
    }

    private void Start()
    {
        
        Invoke("TitleFadeIn", 2);
    }
    private void Update()
    {
        CubeAnimateMove();
        if (TitleFadeIsIn&&Input.anyKey)
        {
            TitleFadeOut();
        }
    }

    void TitleFadeIn()
    {
        TitleFadeIsIn = true;
        TitleScreen.gameObject.SetActive(true);
        FadeInOut(TitleScreen, TitleFadeIsIn, TitleFadeTime);
        TitleScreenButton.onClick.AddListener(TitleFadeOut);
    }
    void TitleFadeOut()
    {
        TitleFadeIsIn = false;
        TitleScreenButton.onClick.RemoveListener(TitleFadeOut);
        FadeInOut(TitleScreen, false, TitleFadeTime);
        //TitleScreen.gameObject.SetActive(false);
        Invoke("ButtonsFadeIn", 2);
    }
    void ButtonsFadeIn()
    {
        CubeZoom(true,100f,1.5f);
        FadeInOut(Buttons,true,TitleFadeTime);
    }
    
    

    private void FadeInOut(CanvasGroup canvasGroup ,bool IsFadeIn,float FadeTime)
    {
        if(IsFadeIn)
        {
            //페이드인 
            Debug.Log("페이드 인");
            canvasGroup.DOFade(1, FadeTime);
        }
        else
        {
            //페이드 아웃
            Debug.Log("페이드 아웃");
            canvasGroup.DOFade(0, FadeTime);
        }
    }

    private void CubeAnimateMove()
    {
        Cube.transform.DORotate(new Vector3(0,0,1),1);
       
    }

    void CubeZoom(bool IsInout,float Scale,float Time)
    {
        if (IsInout)
        {
            Cube.transform.DOScale(Scale, Time);
        }
        else
        {
            Cube.transform.DOScale(-Scale, Time);
        }
    }
    
    void OnButtonGameObject(bool ison,GameObject gameObject)
    {
        gameObject.SetActive(ison);
    }

    void OnButtonStartGame()
    {
        GameManager.Instance._DataManager.saveData=new SaveData();
        GameManager.Instance._DataManager.playerData=new PlayerData();
        
       // GameManager.Instance.LoadScene("LoadingScene");
    }
    void OnButtonLoadGame()
    {
        GameManager.Instance._DataManager.saveData = new SaveData();
        GameManager.Instance._DataManager.playerData = new PlayerData();
        GameManager.Instance._DataManager.DataLoad();
       // GameManager.Instance.LoadScene("LoadingScene");
    }

}
