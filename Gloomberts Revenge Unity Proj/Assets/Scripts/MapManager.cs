using System.Collections;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Player In Scene")]
    public GameObject playerPrefab;
    public Vector3 playerSpawn;
    GameObject player = null;

    [Header("Bot")]
    public GameObject gloombertPrefab;
    public Vector3 gloomSpawn;
    GameObject gloombert = null;

    [Header("Collectables")]
    public GameObject[] keys;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Instantiate(playerPrefab, playerSpawn, Quaternion.identity);
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Wait() {
        yield return new WaitForSeconds(10);
        gloombert = Instantiate(gloombertPrefab, gloomSpawn, Quaternion.identity);
    }
}
