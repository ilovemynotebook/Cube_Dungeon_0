﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NavManager : MonoBehaviour
{
    private int index;
    private GameObject currentPanel;
    [SerializeField]
    private GameObject[] panels;

    void Start()
    {
        SetActivePanel(0);
    }
    public void NextPanel()
    {
        SetActivePanel((int)Mathf.Repeat(++index, panels.Length));
    }
    public void PrevPanel()
    {
        SetActivePanel((int)Mathf.Repeat(--index, panels.Length));
    }
    private void SetActivePanel(int index)
    {
        if (currentPanel != null)
            currentPanel.SetActive(false);

        currentPanel = panels[index];

        currentPanel.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextPanel();
        }
    }

}
