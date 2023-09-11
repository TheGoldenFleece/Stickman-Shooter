using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PassLevelUI : MonoBehaviour
{
    private const string MAIN_MENU_SCENE = "MainMenu";

    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button retryButton;

    private void Start() {
        GameManager.Instance.OnPassLevel += GameManager_OnPassLevel;
        nextLevelButton.onClick.AddListener(() => NextLevel());
        mainMenuButton.onClick.AddListener(() => MainMenu());
        retryButton.onClick.AddListener(() => Retry());

        Hide();
    }

    private void OnDestroy() {
        GameManager.Instance.OnPassLevel -= GameManager_OnPassLevel;
        nextLevelButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.RemoveAllListeners();
        retryButton.onClick.RemoveAllListeners();
    }

    private void GameManager_OnPassLevel(object sender, System.EventArgs e) {
        Show();
    }

    private void NextLevel() {
        Retry();
    }
    private void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void MainMenu() {
        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }

    private void Hide() {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Show() {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
