using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image exitMenu;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            exitMenu.gameObject.SetActive(true);
        }
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }

    public void CancelQuit()
    {
        Time.timeScale = 1f;
        exitMenu.gameObject.SetActive(false);
    }
}
