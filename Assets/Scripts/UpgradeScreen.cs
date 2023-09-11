using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScreen : MonoBehaviour
{
    public static UpgradeScreen Instance;

    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI fireRateText;
    [SerializeField] private Button hideUpgradeScreenButton;
    [SerializeField] private Button[] showUpgradeScreenButton;
    [SerializeField] private Button damageUpgradeButton;
    [SerializeField] private TextMeshProUGUI damageUpgradeCostText;
    [SerializeField] private Button fireRateUpgradeButton;
    [SerializeField] private TextMeshProUGUI fireRateUpgradeCostText;

    [SerializeField] private GameplaySettingsSO gameplaySettingsSO;
    private float damageUpgradeCost;
    private float fireRateUpgradeCost;

    private DataSaver dataSaver;

    private void Awake() {
        if (Instance != null) {
            return;
        }
        Instance = this;
    }

    private void Start() {
        dataSaver = DataSaver.Instance;

        UpdateCost();

        hideUpgradeScreenButton.onClick.AddListener(() => Hide());
        foreach (Button upgradeSceeneButton in showUpgradeScreenButton) {
            upgradeSceeneButton.onClick.AddListener(() => Show());
        }
        damageUpgradeButton.onClick.AddListener(() => UpgradeDamage());
        fireRateUpgradeButton.onClick.AddListener(() => UpgradeFireRate());

        Hide();
    }

    private void UpdateCost() {
        int damageLevel = (int)DataSaver.Instance.Get(DataSaver.Data.Damage);
        damageUpgradeCost = gameplaySettingsSO.damageUpgradeCost * damageLevel;

        int fireRateLevel = (int)DataSaver.Instance.Get(DataSaver.Data.FireRate);
        fireRateUpgradeCost = gameplaySettingsSO.fireRateUpgradeCost * fireRateLevel;
    }

    private void UpgradeDamage() {
        int money = (int)dataSaver.Get(DataSaver.Data.Money);
        if (money >= damageUpgradeCost) {
            float addedDamage = 1.0f;
            dataSaver.Save(DataSaver.Data.Damage, addedDamage);
            dataSaver.Save(DataSaver.Data.Money, -damageUpgradeCost);

            damageText.text = $"DAMAGE ({dataSaver.Get(DataSaver.Data.Damage)})";
            damageUpgradeCostText.text = damageUpgradeCost.ToString();

            UpdateCost();
        }
    }

    private void UpgradeFireRate() {
        int money = (int)dataSaver.Get(DataSaver.Data.Money);
        if (money >= fireRateUpgradeCost) {
            float addedFireRate = .5f;
            dataSaver.Save(DataSaver.Data.FireRate, addedFireRate);
            dataSaver.Save(DataSaver.Data.Money, -fireRateUpgradeCost);

            fireRateText.text = $"FIRE RATE ({dataSaver.Get(DataSaver.Data.FireRate)})";
            fireRateUpgradeCostText.text = fireRateUpgradeCost.ToString();

            UpdateCost();
        }
    }

    private void Show() {
        gameObject.SetActive(true);

        Time.timeScale = 0f;

        damageText.text = $"DAMAGE ({dataSaver.Get(DataSaver.Data.Damage)})";
        damageUpgradeCostText.text = damageUpgradeCost.ToString();

        fireRateText.text = $"FIRE RATE ({dataSaver.Get(DataSaver.Data.FireRate)})";
        fireRateUpgradeCostText.text = fireRateUpgradeCost.ToString();
    }

    private void Hide() {
        Time.timeScale = 1f;

        gameObject.SetActive(false);
    }
}
