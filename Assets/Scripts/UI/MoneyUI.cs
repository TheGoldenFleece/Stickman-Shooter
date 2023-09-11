using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI money;

    private void Start() {
        Display();
    }

    private void Update() {
        Display();
    }

    public void Display() {
        money.text = DataSaver.Instance.Get(DataSaver.Data.Money).ToString();
    }

}
