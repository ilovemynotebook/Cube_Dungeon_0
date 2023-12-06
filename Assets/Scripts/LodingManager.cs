using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LodingManager : MonoBehaviour
{
    [SerializeField] private Slider _loadingBar;
    public float MaxLoadingSpeed;
    DataManager dataManager;
    public PlaneSceneManager planeSceneManager;

    private AsyncOperation _op;
    void Start()
    {
        dataManager = GameManager.Instance._DataManager;
        LoadingScene("Stage" + dataManager.saveData.thisStage);
        StartCoroutine(LoadingRoutine());
    }

    private IEnumerator LoadingRoutine()
    {
        float value = 0;
        while (true)
        {
            if (GameManager.Instance != null)
            {

                if (_loadingBar.value >= 0.9f)
                {
                    _loadingBar.value += MaxLoadingSpeed;
                    if (_loadingBar.value >= 1)
                    {
                        CompleteLoading();
                        yield break;
                    }
                }
                else
                {
                    _loadingBar.value += MaxLoadingSpeed;
                    value = GetLoadingProgress();
                    if (_loadingBar.value > value) { _loadingBar.value = value; }
                }

            }
            yield return null;
        }
    }

    public float GetLoadingProgress()
    {
        if (_op != null)
        {
            return _op.progress;
        }
        return 0;
    }
    public void LoadingScene(string sceneName)
    {
        _op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        _op.allowSceneActivation = false;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void CompleteLoading()
    {
        _op.allowSceneActivation = true;
    }

}
