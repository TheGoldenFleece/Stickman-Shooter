using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameplaySettingsSO gameplaySO;
    [Header("Projectile Settings")]
    [SerializeField] private float multiplierSpeed = 5f;
    [SerializeField] private float timeToLive = 5f;

    private float speed;

    private void OnEnable() {
        Destroy(gameObject, timeToLive);
    }

    private void Start() {
        speed = gameplaySO.playerSpeed * multiplierSpeed;
    }

    void Update()
    {
        Vector3 moveForward;
        if (GameManager.Instance.IsDemo) {
            moveForward = speed* Time.deltaTime * transform.forward;
        }
        else {
            moveForward = speed * Time.deltaTime * PlayerController.Instance.transform.forward;
        }
        transform.Translate(moveForward);
    }
}
