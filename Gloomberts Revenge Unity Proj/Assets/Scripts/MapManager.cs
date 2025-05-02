using UnityEngine;
using System.Collections.Generic;
using Unity.AI.Navigation;

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
    static List<int> numDoors;

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
    public Vector3 greyDoorSpawn;
    public Quaternion greyDoorRotation;
    public Vector3 cyanDoorSpawn1;
    public Quaternion cyanDoorRotation1;
    public Vector3 cyanDoorSpawn2;
    public Quaternion cyanDoorRotation2;
    public Vector3 redDoorSpawn;
    public Quaternion redDoorRotation;
    public Vector3 whiteDoorSpawn;
    public Quaternion whiteDoorRotation;

    // All Keys

    [Header("Keys")]
    public GameObject keyPrefab;
    static List<GameObject> keys;
    public static List<bool> pickedKeys;

    public Vector3 greenKeySpawn;
    public Quaternion greenKeyRotation;
    public Vector3 yellowKeySpawn;
    public Quaternion yellowKeyRotation;
    public Vector3 blueKeySpawn;
    public Quaternion blueKeyRotation;
    public Vector3 greyKeySpawn;
    public Quaternion greyKeyRotation;
    public Vector3 cyanKeySpawn;
    public Quaternion cyanKeyRotation;
    public Vector3 redKeySpawn;
    public Quaternion redKeyRotation;
    public Vector3 whiteKeySpawn;
    public Quaternion whiteKeyRotation;

    [Header("Vent Stuff")]
    public GameObject ventPrefab;
    static List<GameObject> vents;

    public GameObject drillPrefab;
    public static GameObject drill;
    public Vector3 drillSpawn;
    public Quaternion drillRotation;
    public static bool pickedDrill;

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
        numDoors = new List<int>();

        inScene = false;

        surface = navMeshSurfaceObject.GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();

        gloombert = null;
        playerObject = Instantiate(playerPrefab, playerSpawn, Quaternion.identity);
        firstPersonController = playerObject.GetComponent<FirstPersonController>();

        // Green
        GameObject greenDoor = Instantiate(doorPrefab, greenDoorSpawn, greenDoorRotation);
        greenDoor.GetComponent<Renderer>().material.color = Color.green;
        List<GameObject> greenDoors = new List<GameObject>() { greenDoor };
        GameObject greenKey = Instantiate(keyPrefab, greenKeySpawn, greenKeyRotation);
        greenKey.GetComponent<Renderer>().material.color = Color.green;
        keysToDoors.Add(0, greenDoors);
        pickedKeys.Add(false);
        keys.Add(greenKey);
        numDoors.Add(greenDoors.Count);

        // Yellow

        yellowDoor = Instantiate(doorPrefab, yellowDoorSpawn, yellowDoorRotation);
        yellowDoor.GetComponent<Renderer>().material.color = Color.yellow;
        List<GameObject> yellowDoors = new List<GameObject>() {yellowDoor};
        GameObject yellowKey = Instantiate(keyPrefab, yellowKeySpawn, yellowKeyRotation);
        yellowKey.GetComponent<Renderer>().material.color = Color.yellow;
        keysToDoors.Add(1, yellowDoors);
        pickedKeys.Add(false);
        keys.Add(yellowKey);
        numDoors.Add(yellowDoors.Count);

        // Blue
        GameObject blueDoor1 = Instantiate(doorPrefab, blueDoorSpawn1, blueDoorRotation1);
        blueDoor1.GetComponent<Renderer>().material.color = Color.blue;
        GameObject blueDoor2 = Instantiate(doorPrefab, blueDoorSpawn2, blueDoorRotation2);
        blueDoor2.GetComponent<Renderer>().material.color = Color.blue;
        List<GameObject> blueDoors = new List<GameObject>() { blueDoor1, blueDoor2 };
        GameObject blueKey = Instantiate(keyPrefab, blueKeySpawn, blueKeyRotation);
        blueKey.GetComponent<Renderer>().material.color = Color.blue;
        keysToDoors.Add(2, blueDoors);
        pickedKeys.Add(false);
        keys.Add(blueKey);
        numDoors.Add(blueDoors.Count);

        // Grey
        GameObject greyDoor = Instantiate(doorPrefab, greyDoorSpawn, greyDoorRotation);
        greyDoor.GetComponent<Renderer>().material.color = Color.gray;
        List<GameObject> greyDoors = new List<GameObject>() { greyDoor };
        GameObject greyKey = Instantiate(keyPrefab, greyKeySpawn, greyKeyRotation);
        greyKey.GetComponent<Renderer>().material.color = Color.gray;
        keysToDoors.Add(3, greyDoors);
        pickedKeys.Add(false);
        keys.Add(greyKey);
        numDoors.Add(greyDoors.Count);

        // Cyan
        GameObject cyanDoor1 = Instantiate(doorPrefab, cyanDoorSpawn1, cyanDoorRotation1);
        cyanDoor1.GetComponent<Renderer>().material.color = Color.cyan; 
        GameObject cyanDoor2 = Instantiate(doorPrefab, cyanDoorSpawn2, cyanDoorRotation2);
        cyanDoor2.GetComponent<Renderer>().material.color = Color.cyan;
        List<GameObject> cyanDoors = new List<GameObject>() { cyanDoor1, cyanDoor2};
        GameObject cyanKey = Instantiate(keyPrefab, cyanKeySpawn, cyanKeyRotation);
        cyanKey.GetComponent<Renderer>().material.color = Color.cyan;
        keysToDoors.Add(4, cyanDoors);
        pickedKeys.Add(false);
        keys.Add(cyanKey);
        numDoors.Add(cyanDoors.Count);

        // Red
        GameObject redDoor = Instantiate(doorPrefab, redDoorSpawn, redDoorRotation);
        redDoor.GetComponent<Renderer>().material.color = Color.red;
        List<GameObject> redDoors = new List<GameObject>() { redDoor };
        GameObject redKey = Instantiate(keyPrefab, redKeySpawn, redKeyRotation);
        redKey.GetComponent<Renderer>().material.color = Color.red;
        keysToDoors.Add(5, redDoors);
        pickedKeys.Add(false);
        keys.Add(redKey);
        numDoors.Add(redDoors.Count);

        // White
        GameObject whiteDoor = Instantiate(doorPrefab, whiteDoorSpawn, whiteDoorRotation);
        whiteDoor.GetComponent<Renderer>().material.color = Color.white;
        List<GameObject> whiteDoors = new List<GameObject>() { whiteDoor };
        GameObject whiteKey = Instantiate(keyPrefab, whiteKeySpawn, whiteKeyRotation);
        redKey.GetComponent<Renderer>().material.color = new Color(128f, 0f, 128f);
        keysToDoors.Add(6, whiteDoors);
        pickedKeys.Add(false);
        keys.Add(whiteKey);
        numDoors.Add(whiteDoors.Count);

        drill = Instantiate(drillPrefab, drillSpawn, drillRotation);
        pickedDrill = false;

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
        CoroutineRunner.Instance.RunWaitCoroutine();
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
                            UIManager.UpdateIcons(i);
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
                                numDoors[i] -= 1;
                                if(numDoors[i] == 0)
                                {
                                    UIManager.UpdateIcons(i);
                                }
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
                    if (Vector3.Distance(pos, item.transform.position) < 0.01f && pickedDrill)
                    {
                        vents[i] = null;
                        Destroy(vent);
                        UpdateNavMesh();
                    }
                }
            }
        }
        if (item.CompareTag("Drill"))
        {
            Destroy(drill);
            pickedDrill = true;
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
