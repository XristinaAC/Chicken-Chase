using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
 
    void Awake()
    {
        if (SettingsMenu.Instance == null)
        {
            Instantiate(settingsMenu);
        }
    }
}
