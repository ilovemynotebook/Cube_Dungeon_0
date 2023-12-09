using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryDialogManager : MonoBehaviour
{
    public GameObject dialogBoxPrefab; // 대화 상자에 사용될 프리팹
    public Transform dialogBoxParent; // 대화 상자가 생성될 부모 Transform
    public TextMeshProUGUI speakerNameText; // 화자 이름을 나타낼 UI Text
    public TextMeshProUGUI dialogText; // 대화 내용을 나타낼 UI Text
    public float typingSpeed = 0.05f; // 글자가 나타나는 속도
    private bool isDialogActive = false; // 대화 중인지 여부를 나타내는 변수 추가
    public List<GameObject> objectsToPause; // 일시 정지할 다른 오브젝트들
    private List<float> originalTimeScales; // 오브젝트들의 원래 Time.timeScale 값들을 저장
    private Queue<Dialog> dialogQueue = new Queue<Dialog>();
    private bool isTyping = false;
    [System.Serializable]
    public class Dialog
    {
        public string speakerName;
        [TextArea(3, 10)]
        public string dialogMessage;
    }

    void Start()
    {
        //objectsToPause.Add(GameManager.Instance.Player);
        Time.timeScale = 1f; // 게임 시작 시 Time.timeScale을 원래대로 초기화
        //originalTimeScales = new List<float>();
        //foreach (GameObject obj in objectsToPause)
        //{
        //    originalTimeScales.Add(obj.GetComponent<TimeScaleController>().originalTimeScale);
        //}
        dialogBoxPrefab.SetActive(false);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            if (dialogQueue.Count > 0)
            {
                DisplayNextDialog();
            }
            else
            {
                EndDialog();
            }
        }
    }

    public void StartDialog(List<Dialog> dialogList)
    {

        dialogQueue.Clear();
        isDialogActive = true;
        dialogBoxPrefab.SetActive(true);
        Time.timeScale = 0f;
        //// 일시 정지할 다른 오브젝트들의 Time.timeScale 값을 저장하고, 일시 정지
        //for (int i = 0; i < objectsToPause.Count; i++)
        //{
        //    objectsToPause[i].GetComponent<TimeScaleController>().Pause();
        //}

        foreach (Dialog dialog in dialogList)
        {
            dialogQueue.Enqueue(dialog);
        }

        DisplayNextDialog();
    }

    void DisplayNextDialog()
    {
        Dialog currentDialog = dialogQueue.Dequeue();
        dialogBoxPrefab.SetActive(true);
        StartCoroutine(TypeDialog(currentDialog));
    }

    IEnumerator TypeDialog(Dialog dialog)
    {
        isTyping = true;
        speakerNameText.text = dialog.speakerName;
        dialogText.text = "";

        foreach (char letter in dialog.dialogMessage.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
    }

    void EndDialog()
    {
        //// 대화 종료 시 다른 오브젝트들의 Time.timeScale 값을 복원하고, 게임 재개
        //for (int i = 0; i < objectsToPause.Count; i++)
        //{
        //    objectsToPause[i].GetComponent<TimeScaleController>().Resume();
        //}

        Time.timeScale = 1f; // 대화 종료 시 게임 재개
        // 대화가 끝났을 때 수행할 작업 추가
        Debug.Log("End of Dialog");
        isDialogActive = false;
        dialogBoxPrefab.SetActive(false);
        gameObject.SetActive(false);

    }
}
