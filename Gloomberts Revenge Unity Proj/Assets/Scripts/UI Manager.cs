using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject uiSpace;
    public GameObject keyIconPrefab;
    public Sprite keyUnlocked;
    public Sprite keyLocked;
    public Vector3 greenIconPosition;
    public Vector3 yellowIconPosition;
    public Vector3 blueIconPosition;
    public Vector3 purpleIconPosition;
    public Vector3 cyanIconPosition;
    public Vector3 redIconPosition;
    public Vector3 blackIconPosition;

    static List<GameObject> icons;
    static List<Sprite> images;
    void Start()
    {
        uiSpace = GetComponent<Canvas>().gameObject;
        icons = new List<GameObject>();
        images = new List<Sprite>();
        images.Add(keyLocked);
        images.Add(keyUnlocked);
        
        // Green
        GameObject greenIcon = Instantiate(keyIconPrefab, uiSpace.transform);
        greenIcon.GetComponent<RectTransform>().anchoredPosition = greenIconPosition;
        greenIcon.GetComponent<Image>().color = Color.green;
        greenIcon.SetActive(false);
        icons.Add(greenIcon);

        // Yellow
        GameObject yellowIcon = Instantiate(keyIconPrefab, uiSpace.transform);
        yellowIcon.GetComponent<RectTransform>().anchoredPosition = yellowIconPosition;
        yellowIcon.GetComponent<Image>().color = Color.yellow;
        yellowIcon.SetActive(false);
        icons.Add(yellowIcon);

        // Blue
        GameObject blueIcon = Instantiate(keyIconPrefab, uiSpace.transform);
        blueIcon.GetComponent<RectTransform>().anchoredPosition = blueIconPosition;
        blueIcon.GetComponent<Image>().color = Color.blue;
        blueIcon.SetActive(false);
        icons.Add(blueIcon);

        // Purple
        GameObject purpleIcon = Instantiate(keyIconPrefab, uiSpace.transform);
        purpleIcon.GetComponent<RectTransform>().anchoredPosition = purpleIconPosition;
        purpleIcon.GetComponent<Image>().color = new Color(128, 0, 128);
        purpleIcon.SetActive(false);
        icons.Add(purpleIcon);

        // Cyan
        GameObject cyanIcon = Instantiate(keyIconPrefab, uiSpace.transform);
        cyanIcon.GetComponent<RectTransform>().anchoredPosition = cyanIconPosition;
        cyanIcon.GetComponent<Image>().color = Color.cyan;
        cyanIcon.SetActive(false);
        icons.Add(cyanIcon);

        // Red 
        GameObject redIcon = Instantiate(keyIconPrefab, uiSpace.transform);
        redIcon.GetComponent<RectTransform>().anchoredPosition = redIconPosition;
        redIcon.GetComponent<Image>().color = Color.red;
        redIcon.SetActive(false);
        icons.Add(redIcon);

        // Black 
        GameObject blackIcon = Instantiate(keyIconPrefab, uiSpace.transform);
        blackIcon.GetComponent<RectTransform>().anchoredPosition = blackIconPosition;
        blackIcon.GetComponent<Image>().color = Color.black;
        blackIcon.SetActive(false);
        icons.Add(blackIcon);
    }

    public static void UpdateIcons(int i)
    {
        if (MapManager.pickedKeys[i] == true)
        {
            icons[i].GetComponent<Image>().sprite = images[1];
        }
        else if(MapManager.pickedKeys[i] == false)
        {
            icons[i].GetComponent<Image>().sprite = images[0];
            icons[i].SetActive(true);
        }
    }
}
