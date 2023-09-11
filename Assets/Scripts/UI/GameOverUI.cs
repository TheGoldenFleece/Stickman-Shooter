using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    private const string MAIN_MENU_LEVEL = "MainMenu";

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button retryButton;

    private void Start() {
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
        mainMenuButton.onClick.AddListener(() => MainMenu());
        retryButton.onClick.AddListener(() => Retry());

        Hide();
    }

    private void OnDestroy() {
        GameManager.Instance.OnGameOver -= GameManager_OnGameOver;
        mainMenuButton.onClick.RemoveAllListeners();
        retryButton.onClick.RemoveAllListeners();
    }

    private void GameManager_OnGameOver(object sender, System.EventArgs e) {
        Show();
    }

    private void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void MainMenu() {
        SceneManager.LoadScene(MAIN_MENU_LEVEL);
    }
    public void Hide() {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    public void Show() {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
