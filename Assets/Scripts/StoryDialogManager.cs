using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryDialogManager : MonoBehaviour
{
    public GameObject dialogBoxPrefab; // ��ȭ ���ڿ� ���� ������
    public Transform dialogBoxParent; // ��ȭ ���ڰ� ������ �θ� Transform
    public TextMeshProUGUI speakerNameText; // ȭ�� �̸��� ��Ÿ�� UI Text
    public TextMeshProUGUI dialogText; // ��ȭ ������ ��Ÿ�� UI Text
    public float typingSpeed = 0.05f; // ���ڰ� ��Ÿ���� �ӵ�
    private bool isDialogActive = false; // ��ȭ ������ ���θ� ��Ÿ���� ���� �߰�
    public List<GameObject> objectsToPause; // �Ͻ� ������ �ٸ� ������Ʈ��
    private List<float> originalTimeScales; // ������Ʈ���� ���� Time.timeScale ������ ����
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
        Time.timeScale = 1f; // ���� ���� �� Time.timeScale�� ������� �ʱ�ȭ
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
        //// �Ͻ� ������ �ٸ� ������Ʈ���� Time.timeScale ���� �����ϰ�, �Ͻ� ����
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
        //// ��ȭ ���� �� �ٸ� ������Ʈ���� Time.timeScale ���� �����ϰ�, ���� �簳
        //for (int i = 0; i < objectsToPause.Count; i++)
        //{
        //    objectsToPause[i].GetComponent<TimeScaleController>().Resume();
        //}

        Time.timeScale = 1f; // ��ȭ ���� �� ���� �簳
        // ��ȭ�� ������ �� ������ �۾� �߰�
        Debug.Log("End of Dialog");
        isDialogActive = false;
        dialogBoxPrefab.SetActive(false);
        gameObject.SetActive(false);

    }
}
