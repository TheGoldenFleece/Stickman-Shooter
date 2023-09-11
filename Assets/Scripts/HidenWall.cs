using UnityEngine;

public class HidenWall : MonoBehaviour
{
    private static string PROJECTILE_TAG = "Projectile";
    private static string PLAYER_TAG = "Player";

    private bool oneTime = false;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(PROJECTILE_TAG)) {
            Destroy(other.gameObject);
        }

        if (other.CompareTag(PLAYER_TAG) && !oneTime) {
            GameManager.Instance.state = GameManager.State.Cutscene;

            oneTime = true;
        }
    }
}
