using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    GameObject uiSpace;
    public GameObject keyIconPrefab;
    public Image keyUnlocked;
    public Image keyLocked;
    public Vector3 greenIconPositon;
    public Vector3 yellowIconPosition;
    public Vector3 blueIconPositon;
    public Vector3 purpleIconPosition;
    public Vector3 cyanIconPosition;
    public Vector3 redIconPosiiton;
    public Vector3 blackIconPosition;
    void Start()
    {
        uiSpace = GetComponent<Canvas>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
