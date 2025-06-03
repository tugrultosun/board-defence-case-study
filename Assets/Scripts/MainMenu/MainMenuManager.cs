using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
    }



    private void OnPlayButtonClicked()
    {
        playButton.interactable = false;
        StartCoroutine(ProceedToGameScene());
    }

    private IEnumerator ProceedToGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scenes/GameScene", LoadSceneMode.Additive);
        yield return new WaitUntil(() => asyncLoad.isDone);
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("Scenes/MenuScene");
        yield return new WaitUntil(() => asyncUnload.isDone);
    }
}
