using UnityEngine;

public class BossPlatform : MonoBehaviour
{
    private static string PLAYER_TAG = "Player";
    private void OnCollisionEnter(Collision collision) {
        if (!collision.transform.CompareTag(PLAYER_TAG)) return;

        GameManager.Instance.state = GameManager.State.Cutscene;
    }
}
