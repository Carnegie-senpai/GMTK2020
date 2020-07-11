using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    public GameObject CreditsPanel;
    public void QuitGame() 
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = (false);
#else
        Application.Quit();
#endif

    }

    public void PlayGame() 
    {
        SceneManager.LoadScene(1);
    }

    public void ShowCredits()
    {
        if (!CreditsPanel.activeSelf)
            CreditsPanel.SetActive(true);
        else
        {
            CreditsPanel.SetActive(false);
        }
    }
}
