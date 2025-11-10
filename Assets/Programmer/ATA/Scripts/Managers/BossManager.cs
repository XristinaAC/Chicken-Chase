using System.Collections;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform bossHead;
    [SerializeField] private Transform player;

    [Header("Boss Health")]
    [SerializeField] private float maxHealth = 4f;
    [SerializeField] private float deathDelay = 5f;
    [SerializeField] private GameObject deathEffect;

    private float currentHealth;
    private bool isDying;

    private void Awake()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player")?.transform;

        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isDying || player == null || bossHead == null)
            return;
        
        Vector3 lookTarget = player.position;
        lookTarget.y = bossHead.position.y;
        bossHead.LookAt(lookTarget);
    }

    public void TakeDamage(float damage)
    {
        if (isDying) return;

        currentHealth -= damage;


        if (currentHealth <= 0)
            StartCoroutine(DieRoutine());
    }
    
    private IEnumerator DieRoutine()
    {
        if (isDying) yield break;
        isDying = true;
        
        if (deathEffect != null)
            Instantiate(deathEffect, bossHead ? bossHead.position : transform.position, Quaternion.identity);


        yield return new WaitForSeconds(deathDelay);
        
        if (LevelManager.Instance != null)
        {
            yield return LevelManager.Instance.LoadNextLevelAsync();
        }

        isDying = false;
    }
}
