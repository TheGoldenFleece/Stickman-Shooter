using UnityEngine;

public class BossAnimationController : MonoBehaviour
{
    private static string IDLE_ANIMATION = "Idle";
    private static string WALK_ANIMATION = "Walk";

    [SerializeField] private Animator animator;
    [SerializeField] private BossController boss;

    void Update()
    {
        animator.SetBool(IDLE_ANIMATION, !boss.IsMoveEnable);   
        animator.SetBool(WALK_ANIMATION, boss.IsMoveEnable);
    }
}
