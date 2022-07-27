using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryItemsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject inventoryContent;

    [SerializeField] [Tooltip("Put item Prefab here")] 
    private GameObject itemPrefab;

    private Image itemImageTemporary;
    private int itemsAmount;


    private ClothChanger clothChanger;

    public Dictionary<int, string> itemsAvailable;

    private void Awake()
    {
        clothChanger = GameObject.FindObjectOfType<ClothChanger>();
    }
    private void Start()
    {
        UpdateItemsAmount();

        Debug.Log("itemsAmount is: " + itemsAmount);
        // get the data for our button: name, index and IsBottom, which we take from ClothRepo via itemIndex

        if (clothChanger.itemsAvailableIndexArr.Length > 1) // we always have at least 1 item: NoTexture
        {
            for (int i = 1; i < clothChanger.itemsAvailableIndexArr.Length; i++)
            {
                // вытащить индексы и названия available items из ClothManager
                var tempItemIndex = clothChanger.itemsAvailableIndexArr[i];
                var tempItemName = clothChanger.itemsAvailableNameArr[i];

                //var tempItemIndex = itemsAvailable.Keys.ElementAt(i); // take the index of Dict element
                //var tempItemName = itemsAvailable.Values.ElementAt(i); // take the name of Dict element

                itemImageTemporary = itemPrefab.GetComponentInChildren<Image>();

                itemImageTemporary.sprite = Resources.Load<Sprite>
                    ("Items/" + tempItemName); // Item2 is ItemName

                // instantiate the button
                GameObject newItemContent = Instantiate(itemPrefab, new Vector3(0f, 0f, 0f),
                    Quaternion.identity, inventoryContent.transform);
                
                newItemContent.name = "Prefab_" + tempItemName;
                // then, add listener for each button

                Button itemButton = newItemContent.GetComponentInChildren<Button>();

                // in order to set IsBottom (true or false),
                // we have to go to the clothRepo and look the item by index, using cloth manager

                bool tempIsBottom = clothChanger.itemsNamesList[tempItemIndex-1].Item4;

                itemButton.onClick.AddListener(() =>
                    ItemClicked(newItemContent.name, tempItemIndex, tempIsBottom));
            }
        }
    }

    public void ItemClicked(string ButtonName, int itemIndex, bool isBottom)
    {
        // when we click a button, make ClothChanger take care of cloth changing, using itemIndex and IsBottom
        clothChanger.ChangeItem(itemIndex, isBottom);
    }

    public void UpdateItemsAmount()
    {
        itemsAmount = clothChanger.itemsAvailableIndexArr.Length;
    }

}
