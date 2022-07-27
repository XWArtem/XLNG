using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VendorWindowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private TMP_Text itemCostText;
    [SerializeField] private Button[] buttonsSpawned;

    [SerializeField] private GameObject tradeWindowContent;
    private Image itemImageTemporary;
    private DataArchitecture.ClothRepository clothRepository = null;
    [SerializeField] private ItemButtonsManager _itemButtonsManager;
    private ClothChanger _clothChanger = null;

    private int itemIndex;
    private bool itemAvailable;
    private int itemsAmount;
    private bool itemIsBottom;
    private int cost;
    

    private void Awake()
    {
        clothRepository = new DataArchitecture.ClothRepository();
        _clothChanger = FindObjectOfType<ClothChanger>();
        itemsAmount = clothRepository.itemsNamesList.Count;
        _itemButtonsManager.InitializeButtons(itemsAmount);
        buttonsSpawned = new Button[itemsAmount];
        

        if (!tradeWindowContent)
        {
            Debug.Log("tradeWindowContent wasn't found!");
        }
        SpawnItems(itemsAmount);
    }


    private void SpawnItems(int itemsAmount)
    {
        for (int i = 0; i < itemsAmount; i++)
        {
            var tempInt = i;
            itemImageTemporary = itemPanel.GetComponentInChildren<Image>();
 
            itemImageTemporary.sprite = Resources.Load<Sprite>
                ("Items/"+ clothRepository.itemsNamesList[tempInt].Item2); // Item2 is ItemName
            // проверка, €вл€етс€ ли итем уже доступным герою. ≈сли да, нужна доп.логика на префаб

            
            itemCostText = itemPanel.GetComponentInChildren<TMP_Text>();
            itemCostText.text = clothRepository.itemsNamesList[tempInt].Item3.ToString(); // Item3 is its' cost
            GameObject newItemContent = Instantiate(itemPanel, new Vector3(0f, 0f, 0f),
                Quaternion.identity, tradeWindowContent.transform);
            buttonsSpawned[tempInt] = newItemContent.GetComponentInChildren<Button>();
            newItemContent.name = "itemPanel" + i.ToString();

            // вытаскиваем itemIndex и itemName, isBottom дл€ конкретного предмета
            itemIndex = clothRepository.itemsNamesList[tempInt].Item1;
            

            itemAvailable = (Array.Exists(_clothChanger.itemsAvailableIndexArr, x => x == itemIndex));
            itemIsBottom = clothRepository.itemsNamesList[tempInt].Item4;
            cost = clothRepository.itemsNamesList[tempInt].Item3;

            //buttonSpawned[tempInt].onClick.AddListener
            //    (delegate { _itemButtonsManager.Buying(itemIndex, itemAvailable, itemIsBottom, cost); });
            buttonsSpawned[tempInt].onClick.AddListener
                (() => _itemButtonsManager.Buying(itemIndex, itemAvailable, itemIsBottom, cost));
        }
    }
}
