using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.Rendering;

public class TitleManager : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject SettingScreen;
    public CanvasGroup TitleScreen;
    public CanvasGroup Buttons;
    public Button TitleScreenButton;
    public Button SettingButton;
    public Button StartButton;
    public Button LoadButton;
    public float TitleFadeTime;
    public bool TitleFadeIsIn;
    public CinemachineVirtualCamera VirtualCamera;
    float cameraPoint;
    float cameraStartPoint;
    public float cameraSpeed;
    public bool cameraStart;
    [SerializeField]DataManager dataManager;

    public AudioClip audioclip;
    public AudioType audiotype;
    public float volume = 1.0f;
    public float pitch = 1.0f;
    void Awake()
    {
        TitleScreen.gameObject.SetActive(false);
        
    }
    private void OnEnable()
    {
        
        if (dataManager.FileNotExist == true) { LoadButton.interactable = false; }
        SettingButton.onClick.AddListener(OnclickSettingButton);
        StartButton.onClick.AddListener(() => OnButtonStartGame());
        LoadButton.onClick.AddListener(() => OnButtonLoadGame());
    }
    private void OnDisable()
    {
        SettingButton.onClick.RemoveListener(OnclickSettingButton);
        StartButton.onClick.RemoveListener(()=>OnButtonStartGame());
        LoadButton.onClick.RemoveListener(() => OnButtonLoadGame());
    }

    private void Start()
    {
        dataManager = DataManager.Instance;
        cameraPoint = 6;
        cameraStartPoint = 10;
        cameraStart = false;
        VirtualCamera.m_Lens.FieldOfView = cameraStartPoint;
        if (Playable.instance)
        {
            Destroy(Playable.instance.gameObject);
        }

        CubeSoundManager.Instance.PlayAudio(audioclip, audiotype, volume, pitch);

        Invoke("TitleFadeIn", 2);
    }
    private void Update()
    {
        
        if (TitleFadeIsIn&&Input.anyKey)
        {
            TitleFadeOut();
        }
        if (cameraStartPoint>cameraPoint&&cameraStart)
        {
            ZoomCamera();
        }
        if (dataManager.FileNotExist)
        {
            LoadButton.interactable = false;
        }
        else
        {
            LoadButton.interactable = true;
        }
    }

    void TitleFadeIn()
    {
        //Ÿ��Ʋ ���̵���
        TitleFadeIsIn = true;
        TitleScreen.gameObject.SetActive(true);
        FadeInOut(TitleScreen, TitleFadeIsIn, TitleFadeTime);
        TitleScreenButton.onClick.AddListener(TitleFadeOut);
    }
    void TitleFadeOut()
    {
        //Ÿ��Ʋ ���̵�ƿ�
        TitleFadeIsIn = false;
        TitleScreenButton.onClick.RemoveListener(TitleFadeOut);
        FadeInOut(TitleScreen, false, TitleFadeTime);
        //TitleScreen.gameObject.SetActive(false);
        Invoke("ButtonsFadeIn", 2);
    }
    void ButtonsFadeIn()
    {
        cameraStart = true;//ī�޶��� ����
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

  
    
    void OnButtonGameObject(bool ison,GameObject gameObject)
    {
        gameObject.SetActive(ison);
    }

    void OnButtonStartGame()
    {
        //�� ���� ����
        dataManager.NewCloneData();
        dataManager.StageDataChange(dataManager.saveData.thisStage);
        SceneManager.LoadScene("LoadingScene");
    }
    void OnButtonLoadGame()
    {
        //����� ���� ����
        if (dataManager.FileNotExist)
        {
            LoadButton.interactable = false;
            return;
        }
            
        
        dataManager.NewCloneData();
        dataManager.DataLoad();
        SceneManager.LoadScene("LoadingScene");
    }
    void ZoomCamera()
    {
        //ī�޶� startpoint�� �ð��� caemraspeed�� ����
        cameraStartPoint -= Time.deltaTime*cameraSpeed;
        VirtualCamera.m_Lens.FieldOfView = cameraStartPoint;
    }
    void OnclickSettingButton()
    {
        GameManager.Instance.OnSettingPanel(true);
    }

}
