using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAnimationController : MonoBehaviour
{
    private static string HIP_FIRE_ANIMATION = "HipFire";

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void Start() {
        animator.SetBool(HIP_FIRE_ANIMATION, true);
    }
}
