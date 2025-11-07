using UnityEngine;

public class CanonManager : MonoBehaviour
{
    [Header("Canon Settings")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 30f;

    private bool isActive;

    private void OnTriggerEnter(Collider other)
    {
        if (isActive || !other.CompareTag("Player")) return;
        isActive = true;
        
        GameManager.Instance.ChangeState(GameManager.GameState.MiniGame);
        MiniGameManager.Instance.StartMiniGame(this);
    }

    public void FireProjectile(Vector3 targetPos)
    {
        GameObject projObj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile proj = projObj.GetComponent<Projectile>();
        proj.SetTarget(targetPos);
    }


    public void ResetCanon()
    {
        isActive = false;
    }
}