using UnityEngine;

[CreateAssetMenu()]
public class GameplaySettingsSO : ScriptableObject
{
    [Header("Player stats")]
    public float playerSpeed;
    public int currentLevel;
    public float fireRate;
    public int damage;
    public int money;

    public int damageUpgradeCost;
    public int fireRateUpgradeCost;

    [Range(0.01f, 1f)]public float enemiesProbability;
    public int minEnemyHP;
    public int maxEnemyHP;
    public int bossHP;
}
