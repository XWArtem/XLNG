using UnityEngine;

/// <summary>
/// Currently MainHero has two types of items: head and body
/// </summary>
public class MainHero : MonoBehaviour
{
    private GameManager gameManager;
    private ClothManager clothManager;

    int[] currentItems = new int[2];

    private void Awake()
    {
        //find gameManager and clothManager
        if (!gameManager && GameObject.Find("GameManager"))
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        if (!clothManager && GameObject.Find("ClothManager"))
        {
            clothManager = GameObject.Find("ClothManager").GetComponent<ClothManager>();
        }
    }
    private void Start()
    {
        currentItems = gameManager.GetCurrentItems();
        Debug.Log("currentItems[0] is: " + currentItems[0] + "and currentItems[1] is: " + currentItems[1]);
        clothManager.ChangeItem( currentItems[0], false);
        clothManager.ChangeItem(currentItems[1], true);
    }

    public void ChangeHeroPosition(int SceneIndex)
    {
        if (SceneIndex == 3)  // Vendor Scene 
        {
            //RectTransformUtility. transform.position = new Vector3( 290f, - 20f, transform.position.z);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(240, -80);
        }
        if (SceneIndex == 0 || SceneIndex == 2) // Main Menu Scene or Training Scene
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
        if (SceneIndex == 4) // HeroMenu
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(360, -60);
            //GetComponent<RectTransform>().sizeDelta = new Vector2(420, 420);
            GetComponent<RectTransform>().localScale = new Vector2(60f, 60f);
            //GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 5);
        }
    }
}
