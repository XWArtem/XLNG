using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class InventoryItemsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject inventoryContent;
    //[SerializeField] private TMP_Text itemCostText;

    [SerializeField] [Tooltip("Put item Prefab here")] 
    private GameObject itemPrefab;

    private Image itemImageTemporary;
    private int itemsAmount;
    private GameManager gameManager = null;
    private ClothManager clothManager = null;

    public Dictionary<int, string> itemsAvailable;

    private void Awake()
    {
        if (gameManager == null && GameObject.Find("GameManager"))
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        // get itemsAmount here:
        UpdateItemsAmount();
        
        

        // find clothManager
        if (!clothManager && GameObject.Find("ClothManager"))
        {
            clothManager = GameObject.Find("ClothManager").GetComponent<ClothManager>();
        }
    }
    private void Start()
    {
        Debug.Log("itemsAmount is: " + itemsAmount);
        // note, we have to use items available from mainHero (Dictionari named itemsAvailable). This data comes:
        // XLNGRepo -> GameManager -> InventoryItemsSpawner

        // get the data for our button: name, index and IsBottom, which we take from ClothRepo via itemIndex
        if (gameManager.itemsAvailable != null)
        {
            itemsAvailable = gameManager.itemsAvailable;
        }
        else Debug.Log("GameManager hasn't been compiled properly yet!");

        for (int i = 0; i < itemsAmount; i++)
        {
            var tempItemIndex = itemsAvailable.Keys.ElementAt(i); // take the index of Dict element
            var tempItemName = itemsAvailable.Values.ElementAt(i); // take the name of Dict element

            itemImageTemporary = itemPrefab.GetComponentInChildren<Image>();

            // THE PROBLEM IS HERE! :
            itemImageTemporary.sprite = Resources.Load<Sprite>
                ("Items/" + tempItemName); // Item2 is ItemName

            // finally we are ready to instantiate the button
            GameObject newItemContent = Instantiate(itemPrefab, new Vector3(0f, 0f, 0f),
                Quaternion.identity, inventoryContent.transform);
            // set name to each button
            newItemContent.name = "Prefab_" + tempItemName;
            // then, add listener for each button

            Button itemButton = newItemContent.GetComponentInChildren<Button>();

            // in order to set IsBottom (true or false),
            // we have to go to the clothRepo and look the item by index, using cloth manager

            bool tempIsBottom = clothManager.itemsNamesList[tempItemIndex - 1].Item4;

            itemButton.onClick.AddListener(() =>
                ItemClicked(newItemContent.name, tempItemIndex, tempIsBottom));
        }
    }

    public void ItemClicked(string ButtonName, int itemIndex, bool isBottom)
    {
        // when we click a button, make ClothManager take care of cloth changing, using itemIndex and IsBottom or not
        clothManager.ChangeItem(itemIndex, isBottom);
        Debug.Log("Button "+ ButtonName +"was clicked\n and it is bottom: " + isBottom);

    }

    public void UpdateItemsAmount()
    {
        itemsAmount = gameManager.itemsAvailable.Count;
    }

}
