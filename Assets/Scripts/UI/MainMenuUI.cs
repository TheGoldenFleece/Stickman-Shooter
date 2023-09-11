using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private const string GAME_SCENE = "Game";
    private const string BONUS_SCENE = "BONUS";

    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button bonusLevelButton;
    [SerializeField] private TextMeshProUGUI levelPassedText;

    private void Start() {
        startButton.onClick.AddListener(() => StartGame());
        exitButton.onClick.AddListener(() => Exit());
        bonusLevelButton.onClick.AddListener(() => BonusLevel());
        levelPassedText.text = $"LEVELS PASSED: {DataSaver.Instance.Get(DataSaver.Data.CurrentLevel) - 1}";
    }

    private void OnDestroy() {
        startButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
        bonusLevelButton.onClick.RemoveAllListeners();
    }

    private void StartGame() {
        SceneManager.LoadScene(GAME_SCENE);
    }

    private void BonusLevel() {
        SceneManager.LoadScene(BONUS_SCENE);
    }
    private void Exit() {
        Application.Quit();
    }
}
