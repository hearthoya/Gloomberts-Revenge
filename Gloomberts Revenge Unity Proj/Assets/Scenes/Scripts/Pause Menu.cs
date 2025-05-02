using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    static GameObject pauseMenuUI;
    public static bool isPaused;

    private void Start()
    {
        Debug.Log("Pause Menu Good");
        pauseMenuUI = GetComponent<Canvas>().gameObject;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

    public static void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit(); // Doesn't work in editor
    }
}