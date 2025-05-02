using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int level;

    void Start()
    {
        //just commenting so that it hopefully commits
    }
    public void OpenScene()
    {
        if (level == 0)
        {
            SceneManager.LoadScene("StartScreen");
            Cursor.lockState = CursorLockMode.None;
        }
        else if (level == 1) 
        {
            SceneManager.LoadScene("Instruction Screen");
            Cursor.lockState= CursorLockMode.None;
        }
        else if (level == 2)
        {
            SceneManager.LoadScene("Game");
        }



    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("Start Screen");
        }
    }





}
