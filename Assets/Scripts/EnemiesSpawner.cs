using UnityEngine;

public class EnemiesSpawner : MonoBehaviour {
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private Transform leftEnemiesSpawnPos;
    [SerializeField] private Transform rightEnemiesSpawnPos;
    [SerializeField] private Transform endEnemiesSpawnPos;
    [SerializeField] private Transform enemiesContainer;
    [SerializeField] private GameplaySettingsSO gameplaySettingsSO;

    private float enemiesProbability;
    private Vector2 enemySize = new Vector2 (2f, 2f);

    private void Start() {
        enemiesProbability = gameplaySettingsSO.enemiesProbability * DataSaver.Instance.Get(DataSaver.Data.CurrentLevel);

        if (GameManager.Instance.IsBonusLevel) {
            SpawnAllEnemies();
        }

        else {
            SpawnEnemies();
        }
    }

    private void SpawnAllEnemies() {
        int maxEnemiesZ = (int)((endEnemiesSpawnPos.position.z - leftEnemiesSpawnPos.position.z) / enemySize.x);

        int maxEnemiesX = (int)((rightEnemiesSpawnPos.position.x - leftEnemiesSpawnPos.position.x) / enemySize.y);

        Vector2 startSpawnPosition = new Vector3(leftEnemiesSpawnPos.position.x + enemySize.x / 2, leftEnemiesSpawnPos.position.z + enemySize.y);

         for (int i = 0; i < maxEnemiesX; i++) {
            for (int j = 0; j < maxEnemiesZ; j++) {
                Vector3 posToSpawn = new Vector3(startSpawnPosition.x + i * enemySize.x, leftEnemiesSpawnPos.position.y, startSpawnPosition.y + j * enemySize.y);
                Transform enemy = Instantiate(enemyPrefab, posToSpawn, enemyPrefab.rotation);

                enemy.SetParent(enemiesContainer);
            }
        } 
    }

    private void SpawnEnemies() {
        int maxEnemiesZ = (int)((endEnemiesSpawnPos.position.z - leftEnemiesSpawnPos.position.z) / enemySize.x);

        int maxEnemiesX = (int)((rightEnemiesSpawnPos.position.x - leftEnemiesSpawnPos.position.x) / enemySize.y);

        Vector2 startSpawnPosition = new Vector3(leftEnemiesSpawnPos.position.x + enemySize.x / 2, leftEnemiesSpawnPos.position.z + enemySize.y);

        for (int i = 0; i < maxEnemiesX; i++) {
            for (int j = 0; j < maxEnemiesZ; j++) {
                float random = Random.Range(0f, 1f);

                if (random < enemiesProbability) {
                    Vector3 posToSpawn = new Vector3(startSpawnPosition.x + i * enemySize.x, leftEnemiesSpawnPos.position.y, startSpawnPosition.y + j * enemySize.y);
                    Transform enemy = Instantiate(enemyPrefab, posToSpawn, enemyPrefab.rotation);

                    enemy.SetParent(enemiesContainer);
                }
            }
        }
    }

}
