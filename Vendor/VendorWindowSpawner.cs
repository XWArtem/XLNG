using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VendorWindowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private TMP_Text itemCostText;
    [SerializeField] private GameObject tradeWindowContent;
    private Image itemImageTemporary;
    private ClothManager clothManager = null;

    private int itemsAmount;

    //private List<KeyValuePair<string, int>> itemsNamesList = new List<KeyValuePair<string, int>>();
    //private Dictionary<string, int> itemsNamesList = new Dictionary<string, int>();

    

    private void Awake()
    {
        // find clothManager
        if (!clothManager && GameObject.Find("ClothManager"))
        {
            clothManager = GameObject.Find("ClothManager").GetComponent<ClothManager>();
        }

        itemsAmount = 3;
        
        tradeWindowContent = GameObject.Find("TradeWindowContent");
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
                ("Items/"+ clothManager.itemsNamesList[tempInt].Item2); // Item2 is ItemName

            itemCostText = itemPanel.GetComponentInChildren<TMP_Text>();
            itemCostText.text = clothManager.itemsNamesList[tempInt].Item3.ToString(); // Item3 is its' cost
            GameObject newItemContent = Instantiate(itemPanel, new Vector3(0f, 0f, 0f),
                Quaternion.identity, tradeWindowContent.transform);
            newItemContent.name = "itemPanel" + i.ToString();
        }
    }

}
