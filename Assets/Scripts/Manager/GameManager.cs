using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        DataManager.Instance.LoadPlayerData();
    }

    public void ReiniciarNivel()
    {
        DataManager.Instance.ClearPlayerData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CargarSiguienteNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void VolverNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    //Metodo que devuelve el nivel actual
    public int GetNivelActual()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void WinGame()
    {
        Debug.Log("Has ganado el juego");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DataManager.Instance.LoadPlayerData();
    }

}


