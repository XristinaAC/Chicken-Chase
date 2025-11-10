using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject iconPrefab;  
    [SerializeField] private Transform iconParent;  

    private readonly List<GameObject> icons = new();

    private void Awake()
    {
        if (InventoryController.Instance != null)
            InventoryController.Instance.OnInventoryChanged += RefreshUI;
    }

    void OnDisable()
    {
        if (InventoryController.Instance != null)
            InventoryController.Instance.OnInventoryChanged -= RefreshUI;
    }

    public void RefreshUI(List<ProjectileData> projectiles)
    {
        foreach (var icon in icons)
            Destroy(icon);
        icons.Clear();

        foreach (var projectile in projectiles)
        {
            var iconObj = Instantiate(iconPrefab, iconParent);
            var img = iconObj.GetComponent<Image>();
            img.sprite = projectile.icon;
            icons.Add(iconObj);
        }
    }
}
