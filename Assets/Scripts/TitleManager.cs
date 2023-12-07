using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

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
    public CinemachineVirtualCamera VirtualCamera;
    float cameraPoint;
    float cameraStartPoint;
    public float cameraSpeed;
    public bool cameraStart;
    [SerializeField]DataManager dataManager;
    void Awake()
    {
        TitleScreen.gameObject.SetActive(false);
        
    }
    private void OnEnable()
    {
        
        if (dataManager.FileNotExist == true) { LoadButton.interactable = false; }
        SettingButton.onClick.AddListener(() => OnButtonGameObject(true, SettingScreen));
        StartButton.onClick.AddListener(() => OnButtonStartGame());
        LoadButton.onClick.AddListener(() => OnButtonLoadGame());
    }
    private void OnDisable()
    {
        SettingButton.onClick.RemoveListener(()=>OnButtonGameObject(false, SettingScreen));
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
        //타이틀 페이드인
        TitleFadeIsIn = true;
        TitleScreen.gameObject.SetActive(true);
        FadeInOut(TitleScreen, TitleFadeIsIn, TitleFadeTime);
        TitleScreenButton.onClick.AddListener(TitleFadeOut);
    }
    void TitleFadeOut()
    {
        //타이틀 페이드아웃
        TitleFadeIsIn = false;
        TitleScreenButton.onClick.RemoveListener(TitleFadeOut);
        FadeInOut(TitleScreen, false, TitleFadeTime);
        //TitleScreen.gameObject.SetActive(false);
        Invoke("ButtonsFadeIn", 2);
    }
    void ButtonsFadeIn()
    {
        //버튼이 사라진후
        cameraStart = true;//카메라줌 시작
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
        //새 게임 시작
        dataManager.NewCloneData();
        dataManager.StageDataChange(dataManager.saveData.thisStage);
        SceneManager.LoadScene("LoadingScene");
    }
    void OnButtonLoadGame()
    {
        //저장된 게임 시작
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
        //카메라를 startpoint를 시간당 caemraspeed로 줄임
        cameraStartPoint -= Time.deltaTime*cameraSpeed;
        VirtualCamera.m_Lens.FieldOfView = cameraStartPoint;
    }

}
