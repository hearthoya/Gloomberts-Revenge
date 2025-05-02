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
    public Vector3 blueKeySpawn;
    public Vector3 purpleKeySpawn;
    public Vector3 cyanKeySpawn;
    public Vector3 redKeySpawn;
    public Vector3 blackKeySpawn;

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
    public Vector3 ventClosetRoomSpawn;
    public Quaternion ventClosetRoomRotation;
    public Vector3 ventRoomClosetSpawn;
    public Quaternion ventRoomClosetRotation;
    public Vector3 ventRoomVentSpawn;
    public Quaternion ventRoomVentRotation;
    public Vector3 ventVentRoomSpawn;
    public Quaternion ventVentRoomRotation;
    public Vector3 ventBelowSpawn;
    public Quaternion ventBelowRotation;
    public Vector3 ventEndGloomSpawn;
    public Quaternion ventEndGloomRotation;
    public Vector3 ventGloomEndSpawn;
    public Quaternion ventGloomEndRotation;
    public Vector3 ventHallwayEndSpawn;
    public Quaternion ventHallwayEndRotation;

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

        // Blue
        GameObject blueDoor1 = Instantiate(doorPrefab, blueDoorSpawn1, blueDoorRotation1);
        blueDoor1.GetComponent<Renderer>().material.color = Color.blue;
        GameObject blueDoor2 = Instantiate(doorPrefab, blueDoorSpawn2, blueDoorRotation2);
        blueDoor2.GetComponent<Renderer>().material.color = Color.blue;
        List<GameObject> blueDoors = new List<GameObject>() { blueDoor1, blueDoor2 };
        GameObject blueKey = Instantiate(keyPrefab, blueKeySpawn, Quaternion.identity);
        blueKey.GetComponent<Renderer>().material.color = Color.blue;
        keysToDoors.Add(2, blueDoors);
        pickedKeys.Add(false);
        keys.Add(blueKey);

        // Purple
        GameObject purpleDoor = Instantiate(doorPrefab, purpleDoorSpawn, purpleDoorRotation);
        purpleDoor.GetComponent<Renderer>().material.color = new Color(128, 0, 128);
        List<GameObject> purpleDoors = new List<GameObject>() { purpleDoor };
        GameObject purpleKey = Instantiate(keyPrefab, purpleKeySpawn, Quaternion.identity);
        purpleKey.GetComponent<Renderer>().material.color = new Color(128, 0, 128);
        keysToDoors.Add(3, purpleDoors);
        pickedKeys.Add(false);
        keys.Add(purpleKey);

        // Cyan
        GameObject cyanDoor1 = Instantiate(doorPrefab, cyanDoorSpawn1, cyanDoorRotation1);
        cyanDoor1.GetComponent<Renderer>().material.color = Color.cyan; 
        GameObject cyanDoor2 = Instantiate(doorPrefab, cyanDoorSpawn2, cyanDoorRotation2);
        cyanDoor2.GetComponent<Renderer>().material.color = Color.cyan;
        List<GameObject> cyanDoors = new List<GameObject>() { cyanDoor1, cyanDoor2};
        GameObject cyanKey = Instantiate(keyPrefab, cyanKeySpawn, Quaternion.identity);
        cyanKey.GetComponent<Renderer>().material.color = Color.cyan;
        keysToDoors.Add(4, cyanDoors);
        pickedKeys.Add(false);
        keys.Add(cyanKey);

        // Red
        GameObject redDoor = Instantiate(doorPrefab, redDoorSpawn, redDoorRotation);
        redDoor.GetComponent<Renderer>().material.color = Color.red;
        List<GameObject> redDoors = new List<GameObject>() { redDoor };
        GameObject redKey = Instantiate(keyPrefab, redKeySpawn, Quaternion.identity);
        redKey.GetComponent<Renderer>().material.color = Color.red;
        keysToDoors.Add(5, redDoors);
        pickedKeys.Add(false);
        keys.Add(redKey);

        // Black
        GameObject blackDoor = Instantiate(doorPrefab, blackDoorSpawn, blackDoorRotation);
        blackDoor.GetComponent<Renderer>().material.color = Color.black;
        List<GameObject> blackDoors = new List<GameObject>() { blackDoor };
        GameObject blackKey = Instantiate(keyPrefab, blackKeySpawn, Quaternion.identity);
        redKey.GetComponent<Renderer>().material.color = Color.black;
        keysToDoors.Add(6, blackDoors);
        pickedKeys.Add(false);
        keys.Add(blackKey);

        screwdriver = Instantiate(screwdriverPrefab, screwDriverSpawn, Quaternion.identity);
        screwdriver.GetComponent<Renderer>().material.color = new Color(1f, 0.5f, 0f);
        pickedScrewdriver = false;

        vents.Add(Instantiate(ventPrefab, ventStartBigSpawn, ventStartBigRotation));
        vents.Add(Instantiate(ventPrefab, ventBigStartSpawn, ventBigStartRotation));
        vents.Add(Instantiate(ventPrefab, ventClosetRoomSpawn, ventClosetRoomRotation));
        vents.Add(Instantiate(ventPrefab, ventRoomClosetSpawn, ventRoomClosetRotation));
        vents.Add(Instantiate(ventPrefab, ventRoomVentSpawn, ventRoomVentRotation));
        vents.Add(Instantiate(ventPrefab, ventVentRoomSpawn, ventVentRoomRotation));
        vents.Add(Instantiate(ventPrefab, ventBelowSpawn, ventBelowRotation));
        vents.Add(Instantiate(ventPrefab, ventEndGloomSpawn, ventEndGloomRotation));
        vents.Add(Instantiate(ventPrefab, ventGloomEndSpawn, ventGloomEndRotation));
        vents.Add(Instantiate(ventPrefab, ventHallwayEndSpawn, ventHallwayEndRotation));

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
            //CoroutineRunner.Instance.RunWaitCoroutine();
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
