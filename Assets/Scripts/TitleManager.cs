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
    public CanvasGroup TitleScreen;
    public CanvasGroup Buttons;
    public Button TitleScreenButton;
    public float TitleFadeTime;
    public bool TitleFadeIsIn;
    void Awake()
    {
        TitleScreen.gameObject.SetActive(false);
        
    }

    private void Start()
    {
        CubeAnimateMove();
    }
    private void Update()
    {
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
        CubeZoom(true,2f,1.5f);
        FadeInOut(Buttons,true,TitleFadeTime);
    }
    
    

    private void FadeInOut(CanvasGroup canvasGroup ,bool IsFadeIn,float FadeTime)
    {
        if(IsFadeIn)
        {
            //���̵��� 
            Debug.Log("���̵� ��");
            canvasGroup.DOFade(1, FadeTime);
        }
        else
        {
            //���̵� �ƿ�
            Debug.Log("���̵� �ƿ�");
            canvasGroup.DOFade(0, FadeTime);
        }
    }

    private void CubeAnimateMove()
    {
        Debug.Log("ť��ִϸ���Ʈ����");
        Invoke("TitleFadeIn", 2);
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
  

}
