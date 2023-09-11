using TMPro;
using UnityEngine;

public class EnemyHealthPointUI : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private TextMeshProUGUI HPText;

    private void Start() {
        enemy.OnHPChanged += Enemy_OnHPChanged;
    }

    private void OnDestroy() {
        enemy.OnHPChanged -= Enemy_OnHPChanged;
    }

    private void Enemy_OnHPChanged(object sender, Enemy.OnHPChangedEventArgs e) {
        DisplayHP(e.HP);
    }

    private void DisplayHP(int value) {
        HPText.text = value.ToString();
    }
}
