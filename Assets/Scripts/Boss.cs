using UnityEngine;

public class Boss : Enemy
{
    private void Awake() {

        if (GameManager.Instance.IsBonusLevel) {
            maxHP = bonusLevelSO.bossHP;
            HP = bonusLevelSO.bossHP;
            killAward = 0;
        }
        else {
            maxHP = gameplaySettingsSO.bossHP * (int)DataSaver.Instance.Get(DataSaver.Data.CurrentLevel);
            HP = maxHP;
            killAward = maxHP;
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag(PROJECTILE_TAG)) return;

        Destroy(other.gameObject);
        base.BeDamaged();

        if (HP <= 0) {
            DataSaver.Instance.Save(DataSaver.Data.Money, killAward);
            GameManager.Instance.state = GameManager.State.PassLevel;

            Destroy(gameObject);
        }
    }
}
