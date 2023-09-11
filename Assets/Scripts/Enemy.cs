using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private protected string PROJECTILE_TAG = "Projectile";

    public event EventHandler<OnHPChangedEventArgs> OnHPChanged;
    public class OnHPChangedEventArgs : EventArgs {
        public int HP;
    }

    protected void OnHPChanged_Invoker(int newHP) {
        OnHPChanged?.Invoke(this, new OnHPChangedEventArgs() {
            HP = newHP
        });
    }

    [SerializeField] private Transform hitEffectTransformPrefab;
    [SerializeField] protected GameplaySettingsSO gameplaySettingsSO;
    [SerializeField] protected GameplaySettingsSO bonusLevelSO;

    protected int maxHP;
    protected int HP;
    protected int killAward;

    private void Awake() {
        int addedExclusive = 1;

        if (GameManager.Instance.IsBonusLevel) {
            maxHP = bonusLevelSO.maxEnemyHP;
            killAward = maxHP;
        }

        else {
            int minHP = gameplaySettingsSO.minEnemyHP * (int)DataSaver.Instance.Get(DataSaver.Data.CurrentLevel);
            int maxHP = gameplaySettingsSO.maxEnemyHP * (int)DataSaver.Instance.Get(DataSaver.Data.CurrentLevel);


            maxHP = UnityEngine.Random.Range(minHP, maxHP + addedExclusive);
            killAward = maxHP;

        }

        HP = maxHP;
    }

    private void Start() {
        OnHPChanged?.Invoke(this, new OnHPChangedEventArgs() {
            HP = HP
        });
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag(PROJECTILE_TAG)) return;

        BeDamaged();

        Destroy(other.gameObject);

        if (HP <= 0) {
            DataSaver.Instance.Save(DataSaver.Data.Money, killAward);
            Destroy(gameObject);
        }
    }

    protected void BeDamaged() {
        HP -= (int)DataSaver.Instance.Get(DataSaver.Data.Damage);

        Vector3 offset = new Vector3(0, 1, 0);
        Transform hitEffectTransform = Instantiate(hitEffectTransformPrefab, transform.position + offset, Quaternion.identity);

        float destroyEffectDelay = 1.5f;
        Destroy(hitEffectTransform.gameObject, destroyEffectDelay);

        OnHPChanged?.Invoke(this, new OnHPChangedEventArgs() {
            HP = HP
        });
    }
}
