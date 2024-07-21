using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;
    public GameObject m_LoadingScreen;
    public Slider progressBar;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            m_LoadingScreen.SetActive(false);
        }
    }

    public void SwitchToScene(int id)
    {
        m_LoadingScreen.SetActive(true);
        progressBar.value = 0;
        StartCoroutine(SwitchToSceneAsync(id));
    }

    IEnumerator SwitchToSceneAsync(int id)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(id);
        while (!asyncLoad.isDone)
        {
            progressBar.value = asyncLoad.progress;
            yield return null;
        }
        yield return new WaitForSeconds(0.2f); // Puede ajustar el tiempo de espera según sus necesidades
        m_LoadingScreen.SetActive(false);
    }
}
