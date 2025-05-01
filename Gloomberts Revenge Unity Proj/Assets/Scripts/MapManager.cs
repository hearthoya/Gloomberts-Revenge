using System.Collections;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine.AI;
using Unity.AI.Navigation;
using Unity.VisualScripting;

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
    public GameObject navMeshSurfaceObject;
    static NavMeshSurface surface;
    static bool inScene;

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

    [Header("Vent Stuff")]
    public GameObject ventPrefab;
    static List<GameObject> vents;

    public GameObject screwdriverPrefab;
    public static GameObject screwdriver;
    public Vector3 screwDriverSpawn;
    public static bool pickedScrewdriver;

    public Vector3 ventStartBig;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keys = new List<GameObject>();
        doors = new List<GameObject>();
        vents = new List<GameObject>();
        inScene = false;

        surface = navMeshSurfaceObject.GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();

        gloombert = Instantiate(gloombertPrefab, gloomSpawn, Quaternion.identity);
        playerObject = Instantiate(playerPrefab, playerSpawn, Quaternion.identity);
        firstPersonController = playerObject.GetComponent<FirstPersonController>();


        yellowKey = Instantiate(keyPrefab, yellowKeySpawn, Quaternion.identity);
        yellowDoor = Instantiate(doorPrefab, yellowDoorSpawn, Quaternion.identity);
        yellowDoor.tag = "Locked Door";
        keys.Add(yellowKey);
        doors.Add(yellowDoor);

        screwdriver = Instantiate(screwdriverPrefab, screwDriverSpawn, Quaternion.identity);
        pickedScrewdriver = false;

        vents.Add(Instantiate(ventPrefab, ventStartBig, Quaternion.identity));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UpdateNavMesh()
    {
        surface.BuildNavMesh();
    }

    public static void ItemCheck(GameObject item)
    {
        if (item.CompareTag("Key"))
        {
            for (int i = 0; i < keys.Count; i++)
            {
                GameObject key = keys[i];
                if (key != null)
                {
                    Vector3 pos = key.transform.position;
                    if (Vector3.Distance(pos, item.transform.position) < 0.01f)
                    {
                        keys[i] = null;
                        Destroy(key);
                    }
                }
            }
        }

        if (item.CompareTag("Locked Door"))
        {
            for (int i = 0; i < doors.Count; i++)
            {
                GameObject door = doors[i];
                if (door != null)
                {
                    Vector3 pos = door.transform.position;
                    if (Vector3.Distance(pos, item.transform.position) < 0.01f && keys[i] == null)
                    {
                        doors[i] = null;
                        Destroy(door);
                        UpdateNavMesh();
                        CheckIfSpawn();
                    }
                }
            }
        }
        if (item.CompareTag("Door"))
        {
            DoorController openedDoor = item.GetComponent<DoorController>();
            openedDoor.isOpened = !openedDoor.isOpened;
            CoroutineRunner.Instance.RunWaitCoroutine();
            UpdateNavMesh();
        }
        if (item.CompareTag("Vent")) {
            for (int i = 0; i < vents.Count; i++)
            {
                GameObject vent = vents[i];
                if (vent != null)
                {
                    Vector3 pos = vent.transform.position;
                    if (Vector3.Distance(pos, item.transform.position) < 0.01f && pickedScrewdriver)
                    {
                        doors[i] = null;
                        Destroy(vent);
                        UpdateNavMesh();
                    }
                }
            }
        }
        if (item.CompareTag("Screwdriver"))
        {
            Destroy(screwdriver);
            pickedScrewdriver = true;
        }
    }

    static void CheckIfSpawn()
    {
        if (!inScene) { }
    }
}
