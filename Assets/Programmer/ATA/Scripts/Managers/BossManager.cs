using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform bossHead;
    private Transform player;

    [Header("Boss Health")]
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float deathDelay = 5f;
    [SerializeField] private GameObject deathEffect;

    [SerializeField] private float currentHealth;
    private bool isDying;

    private void Start()
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
            DieRoutine();
    }
    
    private async Task DieRoutine()
    {
        if (isDying) return;
        isDying = true;

        if (deathEffect)
            Instantiate(deathEffect, bossHead.position, Quaternion.identity);

        await Task.Delay((int)(deathDelay * 1000));

        if (LevelManager.Instance != null)
            await LevelManager.Instance.LoadNextLevelAsync();
    }
}
