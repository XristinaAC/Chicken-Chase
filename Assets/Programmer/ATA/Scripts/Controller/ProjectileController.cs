using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; 
    [SerializeField] private Transform bossTarget;        

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        if (projectilePrefab != null && bossTarget != null)
        {
            Vector3 spawnPos = transform.position + (other.transform.forward * 3f);
            GameObject newProjectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);


            Projectile proj = newProjectile.GetComponent<Projectile>();
            if (proj != null)
                proj.SetTarget(bossTarget.position);
        }
        Destroy(gameObject);
    }
}