using System.Collections;
using UnityEngine;

public class DemoController : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private Transform projectilePrefab;
    [SerializeField] private Transform positionToFire;
    [SerializeField] private Transform projectilesContainer;

    void Start()
    {
        StartCoroutine(Fire());
    }

    IEnumerator Fire() {
        float fireDelay = 1.0f;
        yield return new WaitForSeconds(fireDelay);

        while (true) {
            Transform projectileTransform = Instantiate(projectilePrefab, positionToFire.position, Quaternion.identity);

            projectileTransform.SetParent(projectilesContainer);

            float oneSecond = 1.0f;

            yield return new WaitForSeconds(oneSecond / DataSaver.Instance.Get(DataSaver.Data.FireRate));
        }
    }
}
