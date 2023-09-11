using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController Instance { private set; get; }
    
    [SerializeField] private float moveSpeed = 2f;
    public bool IsMoveEnable { private set; get; }

    private void Awake() {
        if (Instance != null) {
            return;
        }

        Instance = this;

        IsMoveEnable = false;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnCutscene += GameManager_OnCutscene;
        GameManager.Instance.OnBossFight += GameManager_OnBossFight;
    }

    private void OnDisable() {
        GameManager.Instance.OnCutscene -= GameManager_OnCutscene;
        GameManager.Instance.OnBossFight -= GameManager_OnBossFight;
    }

    private void GameManager_OnCutscene(object sender, System.EventArgs e) {
        transform.LookAt(PlayerController.Instance.transform);
    }

    private void GameManager_OnBossFight(object sender, System.EventArgs e) {
        IsMoveEnable = true;
    }

    void Update()
    {
        MoveAtPlayer();
    }

    private void MoveAtPlayer() {
        if (!IsMoveEnable) return;

        Vector3 target = PlayerController.Instance.transform.position;
        transform.Translate(moveSpeed * Time.deltaTime * target.normalized);
    }
}
