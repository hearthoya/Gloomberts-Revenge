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
    public Vector3 greyIconPosition;
    public Vector3 cyanIconPosition;
    public Vector3 redIconPosition;
    public Vector3 whiteIconPosition;

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

        // grey
        GameObject greyIcon = Instantiate(keyIconPrefab, uiSpace.transform);
        greyIcon.GetComponent<RectTransform>().anchoredPosition = greyIconPosition;
        greyIcon.GetComponent<Image>().color = Color.grey;
        greyIcon.SetActive(false);
        icons.Add(greyIcon);

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

        // White 
        GameObject whiteIcon = Instantiate(keyIconPrefab, uiSpace.transform);
        whiteIcon.GetComponent<RectTransform>().anchoredPosition = whiteIconPosition;
        whiteIcon.GetComponent<Image>().color = Color.white;
        whiteIcon.SetActive(false);
        icons.Add(whiteIcon);
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
