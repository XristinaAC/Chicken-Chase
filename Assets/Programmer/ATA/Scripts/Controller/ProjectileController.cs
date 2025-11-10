using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private ProjectileData projectileData;

    private void FixedUpdate()
    {
        transform.Rotate(0,45 * Time.deltaTime,0, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryController.Instance.AddProjectile(projectileData);
            Destroy(gameObject);
        }
    }
}
