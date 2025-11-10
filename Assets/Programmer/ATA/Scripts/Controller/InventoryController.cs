using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; private set; }

    private List<ProjectileData> collectedProjectiles = new List<ProjectileData>();
    
    public Action<List<ProjectileData>> OnInventoryChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddProjectile(ProjectileData item)
    {
        collectedProjectiles.Add(item);
        OnInventoryChanged?.Invoke(collectedProjectiles);
    }

    public ProjectileData GetNextProjectile()
    {
        if (collectedProjectiles.Count == 0) return null;
        ProjectileData data = collectedProjectiles[0];
        collectedProjectiles.RemoveAt(0);
        OnInventoryChanged?.Invoke(collectedProjectiles);
        return data;
    }
    
    public bool HasProjectile()
    {
        return collectedProjectiles.Count > 0;
    }
    
    public void Clear()
    {
        collectedProjectiles.Clear();
        OnInventoryChanged?.Invoke(collectedProjectiles);
    }
    
}