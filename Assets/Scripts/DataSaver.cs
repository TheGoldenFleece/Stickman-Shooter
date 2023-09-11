using System.IO;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    public static DataSaver Instance;
    
    [SerializeField] private GameplaySettingsSO gameplaySettingsSO;
    [SerializeField] protected GameplaySettingsSO bonusLevelSO;

    private string StickmanDataPath;

    private int startLevelMoney;
    private int levelMoney;

    //private DataJson levelJson;
    private DataJson dataJson;

    public enum Data {
        CurrentLevel,
        FireRate,
        Damage,
        Money
    }

    private void Awake() {
        if (Instance != null) {
            return;
        }
        Instance = this;

        levelMoney = 0;

        StickmanDataPath = Path.Combine(Application.persistentDataPath, "StickmanData.json");
        if (GameManager.Instance.IsBonusLevel) {
            dataJson = new DataJson();

            dataJson.money = bonusLevelSO.money;
            dataJson.fireRate = bonusLevelSO.fireRate;
            dataJson.currentLevel = bonusLevelSO.currentLevel;
            dataJson.damage = bonusLevelSO.damage;
        }
        else {
            Load();
        }
    }


    private void Start() {
        GameManager.Instance.OnPassLevel += GameManager_OnPassLevel;
    }

    private void GameManager_OnPassLevel(object sender, System.EventArgs e) {
        dataJson.currentLevel++;
        if (GameManager.Instance.IsBonusLevel) return;

        File.WriteAllText(StickmanDataPath, JsonUtility.ToJson(dataJson));
    }

    public float Get(Data data) {
        switch (data) {
            case Data.CurrentLevel:
                return (float)dataJson.currentLevel;
            case Data.FireRate:
                return dataJson.fireRate;
            case Data.Damage:
                return (float)dataJson.damage;
            case Data.Money:
                return (float)(dataJson.money);
            default:
                return float.NaN;
        }
    }

    public void Save(Data data, float addedValue = 1.0f) {
        switch (data) {
            case Data.CurrentLevel:
                dataJson.currentLevel += (int)addedValue;
                break;
            case Data.FireRate:
                dataJson.fireRate += addedValue;
                break;
            case Data.Damage:
                dataJson.damage += (int)addedValue;
                break;
            case Data.Money:
                dataJson.money += (int)addedValue;
                break;
            default:
                return;
        }
    }

    public void Load() {
        if (!File.Exists(StickmanDataPath)) {
            dataJson = new DataJson(gameplaySettingsSO.currentLevel, gameplaySettingsSO.fireRate, gameplaySettingsSO.damage, gameplaySettingsSO.money);

            return;
        }

        string jsonData = File.ReadAllText(StickmanDataPath);

        dataJson = JsonUtility.FromJson<DataJson>(jsonData);
    }

    private class DataJson {
        public int currentLevel;
        public float fireRate;
        public int damage;
        public int money;

        public DataJson(int currentLevel, float fireRate, int damage, int money) {
            this.currentLevel = currentLevel;
            this.fireRate = fireRate;
            this.damage = damage;
            this.money = money;
        }

        public DataJson() { }
    }
}
