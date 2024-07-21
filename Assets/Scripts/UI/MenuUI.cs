using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class MenuUI : MonoBehaviour
{
    public GameObject menuConfig;
    public void StartGame()
    {
        DataManager.Instance.ClearPlayerData();

        LoadingScreenManager.Instance.SwitchToScene(1);
    }
    public void Options()
    {
        menuConfig.SetActive(true);
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
