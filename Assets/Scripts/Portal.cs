using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    //public StageManager SceneManager;
    public bool isup;
    public bool NeedKey;
    private void OnEnable()
    {
        //SceneManager=FindAnyObjectByType<StageManager>();
    }

    private void Update()
    {
     
    }
    private void OnTriggerStay(Collider other)
    {
        if (NeedKey)
        {
            if (CanvasManager.Instance.key && Input.GetKey(KeyCode.E))
            {
                Debug.Log("Ű�� ����ϼ̽��ϴ�.");
                NeedKey = false;
                CanvasManager.Instance.key = false;
            }
            else if (CanvasManager.Instance.key == false&&Input.GetKey(KeyCode.E))
            {
                Debug.Log("Ű�� ���������� �ʽ��ϴ�.");
            }
                

        }
        else
        {
            if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
            {
                if (isup)
                {
                    PlaneSceneManager.Instance.PlaneUp();
                }
                else
                {

                    PlaneSceneManager.Instance.PlaneDown();
                }

            }
        }
            
     
    }
}
