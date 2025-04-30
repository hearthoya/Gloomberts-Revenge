using System.Collections;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.PackageManager;

public class MapManager : MonoBehaviour
{
    [Header("Player In Scene")]
    public GameObject playerPrefab;
    public Vector3 playerSpawn;
    GameObject playerObject;
    FirstPersonController firstPersonController;

    [Header("Bot")]
    public GameObject gloombertPrefab;
    public Vector3 gloomSpawn;
    GameObject gloombert = null;

    [Header("Door Stuff")]
    public GameObject keyPrefab;
    public GameObject doorPrefab;

    // All Locked Doors
    static List<GameObject> doors;
    GameObject yellowDoor;
    public Vector3 yellowDoorSpawn;

    // All Keys

    static List<GameObject> keys;
    GameObject yellowKey;
    public Vector3 yellowKeySpawn;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keys = new List<GameObject>();
        doors = new List<GameObject>();
        playerObject = Instantiate(playerPrefab, playerSpawn, Quaternion.identity);
        firstPersonController = playerObject.GetComponent<FirstPersonController>();
        StartCoroutine(Wait());
        yellowKey = Instantiate(keyPrefab, yellowKeySpawn, Quaternion.identity);
        yellowDoor = Instantiate(doorPrefab, yellowDoorSpawn, Quaternion.identity);
        yellowDoor.tag = "Locked Door";
        keys.Add(yellowKey);
        doors.Add(yellowDoor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Wait() {
        yield return new WaitForSeconds(10);
        gloombert = Instantiate(gloombertPrefab, gloomSpawn, Quaternion.identity);
    }

    public static void ItemCheck(GameObject item)
    {
        for (int i = 0; i < keys.Count; i++) {
            GameObject key = keys[i];
            if (key != null)
            {
                Vector3 pos = key.transform.position;
                if (pos == item.transform.position)
                {
                    keys[i] = null;
                    Destroy(key);
                }
            }
        }

        for (int i = 0; i < doors.Count; i++)
        {
            GameObject door = doors[i];
            if (door != null)
            {
                Vector3 pos = door.transform.position;
                if (pos == item.transform.position && keys[i] == null)
                {
                    keys[i] = null;
                    Destroy(door);
                }
            }
        }

        if (item.CompareTag("Door"))
        {
            DoorController normalDoor = item.GetComponent<DoorController>();
            normalDoor.isOpened = !normalDoor.isOpened;
        }

    }
}
