using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.UI
{
    public class EndGameScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup winScreen;
        [SerializeField] private CanvasGroup loseScreen;
        [SerializeField] private Button mainMenuButton;

        private void Awake()
        {
            winScreen.gameObject.SetActive(false);
            loseScreen.gameObject.SetActive(false);
            mainMenuButton.onClick.AddListener(OnMainMenuClick);
        }

        public void InitializeWinPanel()
        {
            winScreen.gameObject.SetActive(true);
        }
    
        public void InitializeLosePanel()
        {
            loseScreen.gameObject.SetActive(true);
        }

        private void OnMainMenuClick()
        {
            mainMenuButton.interactable = false;
            StartCoroutine(ProceedToMainMenu());
        }

        private IEnumerator ProceedToMainMenu()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scenes/MenuScene", LoadSceneMode.Additive);
            yield return new WaitUntil(() => asyncLoad.isDone);
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("Scenes/GameScene");
            yield return new WaitUntil(() => asyncUnload.isDone);
        }
    }
}
