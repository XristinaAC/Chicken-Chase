using UnityEngine;

public class CanonManager : MonoBehaviour
{
    [Header("Canon Settings")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 30f;

    private bool isActive;

    private void OnTriggerEnter(Collider other)
    {
        if (isActive || !other.CompareTag("Player")) return;
        isActive = true;

        GameManager.Instance.ChangeState(GameManager.GameState.MiniGame);
        MiniGameManager.Instance.StartMiniGame(this);
    }

    public void FireProjectile(Vector3 targetPos, GameObject projectilePrefab)
    {
        if (projectilePrefab == null) return;
        

        GameObject projObj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        var proj = projObj.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.SetTarget(targetPos);
        }
        else
        {
            Rigidbody rb = projObj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 dir = (targetPos - firePoint.position).normalized;
                rb.velocity = dir * projectileSpeed;
            }
        }
    }

    public void ResetCanon() => isActive = false;
}