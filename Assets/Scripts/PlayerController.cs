using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { private set; get; }
    private static string ENEMY_TAG = "Enemy";

    public bool EnabledHipFire { private set; get; }

    [Header("Projectile")]
    [SerializeField] private Transform projectilePrefab;
    [SerializeField] private Transform positionToFire;
    [SerializeField] private Transform projectilesContainer;


    private IEnumerator fireCoroutine;

    private void Awake() {
        if (Instance != null) {
            return;
        }
        Instance = this;

        EnabledHipFire = false;
        fireCoroutine = Fire();
    }

    private void OnEnable() {
        GameManager.Instance.OnGameRun += GameManager_OnGameRun;
        GameManager.Instance.OnCutscene += GameManager_OnCutscene;
        GameManager.Instance.OnBossFight += GameManager_OnBossFight;
    }

    private void OnDisable() {
        GameManager.Instance.OnGameRun -= GameManager_OnGameRun;
        GameManager.Instance.OnCutscene -= GameManager_OnCutscene;
        GameManager.Instance.OnBossFight -= GameManager_OnBossFight;
    }

    private void GameManager_OnCutscene(object sender, EventArgs e) {
        StopCoroutine(fireCoroutine);
        transform.LookAt(BossController.Instance.transform);
    }

    private void GameManager_OnBossFight(object sender, EventArgs e) {
        EnabledHipFire = true;
        StartCoroutine(fireCoroutine);
    }

    private void GameManager_OnGameRun(object sender, EventArgs e) {
        StartCoroutine(fireCoroutine);
    }

    IEnumerator Fire() {
        while (true) {
            Transform projectileTransform = Instantiate(projectilePrefab, positionToFire.position, Quaternion.identity);

            projectileTransform.SetParent(projectilesContainer);

            float oneSecond = 1.0f;

            yield return new WaitForSeconds(oneSecond / DataSaver.Instance.Get(DataSaver.Data.FireRate));
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (!collision.transform.CompareTag(ENEMY_TAG)) return;

        GameManager.Instance.state = GameManager.State.GameOver;
    }
}

