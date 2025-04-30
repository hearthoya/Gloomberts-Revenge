using UnityEngine;
using System.Collections;

public class CoroutineRunner : MonoBehaviour
{
    public static CoroutineRunner Instance;

    void Awake() { 
        Instance = this; 
    }

    public void RunWaitCoroutine() => StartCoroutine(Wait());

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }
}