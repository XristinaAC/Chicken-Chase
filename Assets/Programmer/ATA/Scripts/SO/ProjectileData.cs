using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectiles/Projectile Item")]
public class ProjectileData : ScriptableObject
{
    public string itemName;
    public GameObject projectilePrefab; 
    public Sprite icon;     
}
