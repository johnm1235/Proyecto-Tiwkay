using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class MenuUI : MonoBehaviour
{
    public void StartGame()
    {
        DataManager.Instance.ClearPlayerData();
        SceneManager.LoadScene(1);
    }
    public void Options()
    {

    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

    }
}
