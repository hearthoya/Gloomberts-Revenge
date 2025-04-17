using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Player In Scene")]
    public GameObject playerPrefab;
    public Vector3 playerSpawn;
    GameObject player;

    [Header("Bot")]
    public GameObject gloombertPrefab;
    public Vector3 gloomSpawn;

    [Header("Collectables")]
    public GameObject[] keys;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Instantiate(playerPrefab, playerSpawn, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
