using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float speed = 10f;
    
    public enum Direction
    {
        Right,
        Forward,
        Left
    }


    
    
    
    private void Update()
    {

        transform.Translate(Vector3.right * (Time.deltaTime * speed));

    }

    private void Die()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.ChangeState(GameManager.GameState.GameOver);
    }

  
    
    
  


}