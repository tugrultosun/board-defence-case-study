using System.Collections;
using Save;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private TextMeshProUGUI levelText;

        private SaveManager saveManager;
        private void Awake()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
            saveManager = new SaveManager();
            levelText.SetText("Level " + saveManager.LoadLevel().Level.ToString());
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
}
