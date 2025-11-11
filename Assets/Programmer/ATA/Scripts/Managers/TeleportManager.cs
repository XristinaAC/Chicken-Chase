using System;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private Transform[] teleportPoints;
    private Transform _playerTransform;

    private int currentTargetIndex = 0;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        
    }

    public void TeleportToNextPoint()
    {
        if (_playerTransform == null || teleportPoints == null || teleportPoints.Length == 0)
            return;

        Transform targetPoint = teleportPoints[currentTargetIndex];

        Rigidbody rb = _playerTransform.GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = Vector3.zero;

        _playerTransform.position = targetPoint.position;
        _playerTransform.rotation = targetPoint.rotation; 

        currentTargetIndex = (currentTargetIndex + 1) % teleportPoints.Length;
    
    }
}