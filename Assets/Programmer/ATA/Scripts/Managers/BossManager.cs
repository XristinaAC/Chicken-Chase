
    using System;
    using UnityEngine;

    public class BossManager : MonoBehaviour
    {
        [SerializeField] private Transform bossHead;
        [SerializeField] private Transform player;
        [SerializeField] private float maxHealth = 4;
        [SerializeField] private float currentHealth;
        
        private void Awake()
        {
            currentHealth = maxHealth;
        }

        private void Update()
        {
            Vector3 lookTarget = player.position;
            lookTarget.y = bossHead.position.y;
            
            bossHead.LookAt(lookTarget);
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            MiniGameManager.Instance.ShowHitText();
            if (currentHealth <= 0)
                Die();
        }

        void Die()
        {
            
        }
    }
