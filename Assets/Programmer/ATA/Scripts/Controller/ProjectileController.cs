using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; 
    [SerializeField] private Transform bossTarget;        

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<PlayerManager2>().GetComponent<Animator>().SetBool("isAttacking", true);

        if (projectilePrefab != null && bossTarget != null)
        {
            //Vector3 spawnPos = transform.position + (other.transform.forward * 3f);
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Projectile proj = newProjectile.GetComponent<Projectile>();
            if (proj != null)
                proj.SetTarget(bossTarget.position);
        }
        StartCoroutine(StopAnimation(other));
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<PlayerManager2>().GetComponent<Animator>().SetBool("isAttacking", true);
    }

    IEnumerator StopAnimation(Collider p)
    {
        yield return new WaitForSeconds(.1f);
        p.GetComponent<PlayerManager2>().GetComponent<Animator>().SetBool("isAttacking", false);
        Destroy(gameObject);
    }
}