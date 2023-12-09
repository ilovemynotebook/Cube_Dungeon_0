using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogObject : MonoBehaviour
{
    public StoryDialogManager dialogManager;
    public List<StoryDialogManager.Dialog> storyDialog;

    private void Start()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogManager.StartDialog(storyDialog);
        }
    }
}
