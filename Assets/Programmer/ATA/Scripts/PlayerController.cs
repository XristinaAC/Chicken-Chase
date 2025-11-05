using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float speed = 10f;

    private void Update()
    {

        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;

        transform.Translate(Vector3.right * (Time.deltaTime * speed));

    }

    private void Die()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.ChangeState(GameManager.GameState.GameOver);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obstacle"))
            Die();
    }

  
    
    
  


}