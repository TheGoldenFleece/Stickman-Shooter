using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;

    private void Start() {
        Show();
    }

    private void Show() {
        levelText.text = $"LEVEL {DataSaver.Instance.Get(DataSaver.Data.CurrentLevel)}";
    }
}
