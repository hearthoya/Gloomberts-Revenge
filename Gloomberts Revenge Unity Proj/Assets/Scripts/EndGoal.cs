using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGoal : MonoBehaviour
{
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Winning Scene");
        }
    }
}
