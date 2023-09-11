using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameplaySettingsSO gameplaySO;

    private float moveSpeed;
    private float turnSpeed = .5f;
    private float moveThreshold = .25f;
    public bool EnabledMovement { private set; get; }

    private float touchDelta;
    private float fallTreshold = -.5f;
    private bool fellDown;

    [SerializeField] private PlayerInputManager playerInputManager;

    private void Awake() {
        fellDown = false;
    }

    private void OnEnable() {
        GameManager.Instance.OnGameRun += GameManager_OnGameRun;
        GameManager.Instance.OnCutscene += GameManager_OnCutscene;
        playerInputManager.OnPlayerInput += PlayerInputManager_OnPlayerInput;
        moveSpeed = gameplaySO.playerSpeed;
    }

    private void OnDisable() {
        GameManager.Instance.OnGameRun -= GameManager_OnGameRun;
        GameManager.Instance.OnCutscene -= GameManager_OnCutscene;
        playerInputManager.OnPlayerInput -= PlayerInputManager_OnPlayerInput;
        moveSpeed = gameplaySO.playerSpeed;
    }

    private void GameManager_OnCutscene(object sender, System.EventArgs e) {
        EnabledMovement = false;
    }

    private void PlayerInputManager_OnPlayerInput(object sender, PlayerInputManager.OnPlayerInputEventArgs e) {
        touchDelta = e.touchDelta;
    }

    private void GameManager_OnGameRun(object sender, System.EventArgs e) {
        EnabledMovement = true;
    }

    private void Update() {
        if (!fellDown && transform.position.y < fallTreshold) {
            fellDown = true;

            GameManager.Instance.state = GameManager.State.GameOver;
        }

        if (EnabledMovement) {
            MoveForward();
            MoveAside();
        }
    }

    private void MoveForward() {
        Vector3 moveZ = moveSpeed * Time.deltaTime * Vector3.forward;
        transform.Translate(moveZ);
    }

    private void MoveAside() {
        Vector3 moveX = Vector3.zero;

        if (Mathf.Abs(touchDelta) > moveThreshold) {
            moveX = new Vector3(touchDelta, 0, 0);
        }

        transform.Translate(turnSpeed * Time.deltaTime * moveX);
    }
}
