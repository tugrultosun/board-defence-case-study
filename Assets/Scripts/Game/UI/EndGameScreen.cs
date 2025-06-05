using UnityEngine;

namespace Game.UI
{
    public class EndGameScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup winScreen;
        [SerializeField] private CanvasGroup loseScreen;

        private void Awake()
        {
            winScreen.gameObject.SetActive(false);
            loseScreen.gameObject.SetActive(false);
        }

        public void InitializeWinPanel()
        {
            winScreen.gameObject.SetActive(true);
        }
    
        public void InitializeLosePanel()
        {
            loseScreen.gameObject.SetActive(true);
        }
    }
}
