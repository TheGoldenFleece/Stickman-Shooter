using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {
    private static string IDLE_ANIMATION = "Idle";
    private static string RUN_ANIMATION = "Run";
    private static string HIP_FIRE_ANIMATION = "HipFire";

    [SerializeField] private PlayerMovement playerMovement;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetBool(RUN_ANIMATION, playerMovement.EnabledMovement);
        animator.SetBool(IDLE_ANIMATION, !playerMovement.EnabledMovement);
        animator.SetBool(HIP_FIRE_ANIMATION, PlayerController.Instance.EnabledHipFire);
    }
}
