using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPanelOpener : MonoBehaviour
{
    //Open chosen panel
    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);

    }
}
