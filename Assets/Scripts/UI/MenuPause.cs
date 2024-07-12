using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPause : MonoBehaviour
{
    public GameObject pauseMenuUI;  
    public static bool GameIsPaused = false;

    public void Start()
    {
        Resume(); 
    }

    void Update()
    {
        // Detectar cuando el jugador presiona la tecla de pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);  
        Time.timeScale = 1f; 
        GameIsPaused = false;

        // Bloquear el cursor y hacerlo invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);  
        Time.timeScale = 0f; 
        GameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
