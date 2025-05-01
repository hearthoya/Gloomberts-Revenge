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
    GameObject gloombert;
    public GameObject navMeshSurfaceObject;
    static NavMeshSurface surface;
    static bool inScene;

    [Header("Doors Stuff")]
    public GameObject doorPrefab;
    static Dictionary<int, List<GameObject>> keysToDoors;

    // All Locked Doors
    public Vector3 greenDoorSpawn;
    public Quaternion greenDoorRotation;
    GameObject yellowDoor;
    public Vector3 yellowDoorSpawn;
    public Quaternion yellowDoorRotation;
    public Vector3 blueDoorSpawn1;
    public Quaternion blueDoorRotation1;
    public Vector3 blueDoorSpawn2;
    public Quaternion blueDoorRotation2;
    public Vector3 purpleDoorSpawn;
    public Quaternion purpleDoorRotation;
    public Vector3 cyanDoorSpawn1;
    public Quaternion cyanDoorRotation1;
    public Vector3 cyanDoorSpawn2;
    public Quaternion cyanDoorRotation2;
    public Vector3 redDoorSpawn;
    public Quaternion redDoorRotation;
    public Vector3 blackDoorSpawn;
    public Quaternion blackDoorRotation;

    // All Keys

    [Header("Keys")]
    public GameObject keyPrefab;
    static List<GameObject> keys;
    static List<bool> pickedKeys;

    public Vector3 greenKeySpawn;
    public Vector3 yellowKeySpawn;

    [Header("Vent Stuff")]
    public GameObject ventPrefab;
    static List<GameObject> vents;

    public GameObject screwdriverPrefab;
    public static GameObject screwdriver;
    public Vector3 screwDriverSpawn;
    public static bool pickedScrewdriver;

    public Vector3 ventStartBigSpawn;
    public Quaternion ventStartBigRotation;
    public Vector3 ventBigStartSpawn;
    public Quaternion ventBigStartRotation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keysToDoors = new Dictionary<int, List<GameObject>>();
        vents = new List<GameObject>();
        pickedKeys = new List<bool>();
        keys = new List<GameObject>();

        inScene = false;

        surface = navMeshSurfaceObject.GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();

        gloombert = null;
        playerObject = Instantiate(playerPrefab, playerSpawn, Quaternion.identity);
        firstPersonController = playerObject.GetComponent<FirstPersonController>();

        // Yellow
        
        yellowDoor = Instantiate(doorPrefab, yellowDoorSpawn, yellowDoorRotation);
        yellowDoor.GetComponent<Renderer>().material.color = Color.yellow;
        List<GameObject> yellowDoors = new List<GameObject>() {yellowDoor};
        GameObject yellowKey = Instantiate(keyPrefab, yellowKeySpawn, Quaternion.identity);
        yellowKey.GetComponent<Renderer>().material.color = Color.yellow;
        keysToDoors.Add(0, yellowDoors);
        pickedKeys.Add(false);
        keys.Add(yellowKey);

        // Green
        GameObject greenDoor = Instantiate(doorPrefab, greenDoorSpawn, greenDoorRotation);
        greenDoor.GetComponent<Renderer>().material.color = Color.green;
        List<GameObject> greenDoors = new List<GameObject>() { greenDoor };
        GameObject greenKey = Instantiate(keyPrefab, greenKeySpawn, Quaternion.identity);
        greenKey.GetComponent<Renderer>().material.color = Color.green;
        keysToDoors.Add(1, greenDoors);
        pickedKeys.Add(false);
        keys.Add(greenKey);

        screwdriver = Instantiate(screwdriverPrefab, screwDriverSpawn, Quaternion.identity);
        pickedScrewdriver = false;

        vents.Add(Instantiate(ventPrefab, ventStartBigSpawn, ventStartBigRotation));
        vents.Add(Instantiate(ventPrefab, ventBigStartSpawn, ventBigStartRotation));

    }

    // Update is called once per frame
    void Update()
    {
        CheckIfSpawn();
    }

    public static void UpdateNavMesh()
    {
        surface.BuildNavMesh();
    }

    public static void ItemCheck(GameObject item)
    {
        if (item.CompareTag("Key"))
        {
            for (int i = 0; i < pickedKeys.Count; i++)
            {
                if (!pickedKeys[i])
                {
                    GameObject key = keys[i];
                    if (key != null)
                    {
                        Vector3 pos = key.transform.position;
                        if (Vector3.Distance(pos, item.transform.position) < 0.01f)
                        {
                            keys[i] = null;
                            pickedKeys[i] = true;
                            Destroy(key);
                        }
                    }
                }
            }
        }

        if (item.CompareTag("Locked Door"))
        {
            for (int i = 0; i < pickedKeys.Count; i++)
            {
                if (pickedKeys[i])
                {
                    List<GameObject> doors = keysToDoors[i];
                    for (int j = 0; j < doors.Count; j++)
                    {
                        GameObject door = doors[j];
                        if (door != null) {
                            Vector3 pos = door.transform.position;
                            if (Vector3.Distance(pos, item.transform.position) < 0.01f && keys[i] == null)
                            {
                                doors[j] = null;
                                Destroy(door);
                                UpdateNavMesh();
                            }
                        }
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
                        vents[i] = null;
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

    void CheckIfSpawn()
    {
        if (!inScene && yellowDoor == null) {
            gloombert = Instantiate(gloombertPrefab, gloomSpawn, Quaternion.identity);
            inScene = true;
        }
    }
}
