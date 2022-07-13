using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    private bool gameNavigationInitialized;
    [SerializeField] private GameObject gameNavigationGameObject;

    private DataArchitecture.XLNGRepository xlngRepository;
    private DataArchitecture.XLNGInteractor xlngInteractor;

    private int coins;
    private int crystals;
    private int wordsBankLevelProgress;
    private string itemsAvailableIndex;
    private string itemsAvailableName;

    private int[] tempitemsAvailableIndex;
    //private List<int> tempitemsAvailableIndexTest = new List<int> { 1, 2};
    private string[] tempitemsAvailableName;

    public Dictionary<int, string> itemsAvailable = new Dictionary<int, string>();

    private void Awake()
    {
        // add GameNavigation on the scene and make sure it never duplicates
        if (!gameNavigationInitialized)
        {
            Debug.Log("GameNavigation is: " + gameNavigationGameObject.name);
            Instantiate(gameNavigationGameObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
            gameNavigationInitialized = true;
            
        }

        xlngRepository = new DataArchitecture.XLNGRepository();
        xlngRepository.Initialize();
        xlngInteractor = new DataArchitecture.XLNGInteractor(xlngRepository);
    }



    private void Start()
    {
        coins = xlngRepository.Coins;
        crystals = xlngRepository.Crystals;
        wordsBankLevelProgress = xlngRepository.WordsBankLevelProgress;
        itemsAvailableIndex = xlngRepository.ItemsAvailableIndex;
        itemsAvailableName = xlngRepository.ItemsAvailableName;

        // set Dictionary values based on strings from Repo:
        // first, make sure strings' Length are equal. Otherwise something went wrong
        if (itemsAvailableIndex.Length != 0 && itemsAvailableName.Length != 0)
        {
            // count items of each string to compare them
            int count1 = itemsAvailableIndex.Count(i => i == ',');
            int count2 = itemsAvailableName.Count(i => i == ',');
            if (count1 != count2)
            {
                Debug.Log("Strings' Length are not equal, check them! \n" +
                    itemsAvailableIndex.Length + "and" + itemsAvailableName.Length);
            }
            else
            {
                Debug.Log("itemsAvailableIndex is: " + itemsAvailableIndex);
                itemsAvailableIndex = itemsAvailableIndex.Remove(0, 1); // remove the first ','
                itemsAvailableName = itemsAvailableName.Remove(0, 1);  // remove the first ','
                var tempitemsAvailableIndexString = itemsAvailableIndex.Split(',');

                tempitemsAvailableIndex = 
                    Array.ConvertAll(tempitemsAvailableIndexString, int.Parse);

                tempitemsAvailableName =
                    itemsAvailableName.Split(new[] { ',' }, System.StringSplitOptions.None);
            }
            // Fill our Dict with indexes and names of Items Available:
            if (tempitemsAvailableIndex != null)
            {
                for (int i = 0; i < tempitemsAvailableIndex.Length; i++)
                {
                    if (itemsAvailable.ContainsKey(tempitemsAvailableIndex[i]))
                    {
                        break;
                    }
                    else
                    {
                        itemsAvailable.Add(tempitemsAvailableIndex[i], tempitemsAvailableName[i]);
                    }
                }
            }
        }
        else Debug.Log("itemAvailableIndex or itemAvailableName are null");
    }

    public void ResetAllStats()
    {
        Debug.Log("All stats are reset");
        xlngInteractor.ResetAllStats();

        coins = xlngRepository.Coins;
        crystals = xlngRepository.Crystals;
        wordsBankLevelProgress = xlngRepository.WordsBankLevelProgress;
        itemsAvailableIndex = xlngRepository.ItemsAvailableIndex;
        itemsAvailableName = xlngRepository.ItemsAvailableName;
        tempitemsAvailableIndex = null;
        itemsAvailable.Clear();
        Debug.Log("My List itemsAvailable contains " + itemsAvailable.Count + " items now");
    }
    public void AddItem(int itemIndex, string itemName)
    {
        // first, check if the item already exists in our itemsAvailable List
        if (itemsAvailable.ContainsKey(itemIndex))
        {
            Debug.Log("Item already exists in the List of ItemsAvailable");
        }
        // if everything's fine and it doesn't exist yet. Add it to our List and Save in Repo:
        else
        {
            itemsAvailable.Add(itemIndex, itemName);
            xlngInteractor.AddItem(itemIndex, itemName);
            // update items in InventoryItemsSpawner to see them in our inventory in the future

            // try to find InventoryItemsSpawner. If it's not on the scene, skip this part.
            // When we open the Scene of heroMenu next time, it will refresh
            if (GameObject.Find("InventoryPanel"))
            {
                var inventoryItemsSpawner = FindObjectOfType<InventoryItemsSpawner>();
                inventoryItemsSpawner.UpdateItemsAmount();
            } 
            
        }
        Debug.Log("My List itemsAvailable contains " + itemsAvailable.Count + " items now");
    }
    public bool ItemIsAvailable(int itemIndex)
    {
        return itemsAvailable.ContainsKey(itemIndex);
    }

    public int[] GetCurrentItems()
    {
        int[] currentItems = new int[2];
        currentItems[0] = xlngRepository.CurrentItemTop; //0
        currentItems[1] = xlngRepository.CurrentItemBottom; //1
        return currentItems;
    }
    // the method ItemsChanged comes here from ClothManager and then goes to the XLNGInteractor:
    public void ItemsChanged(int itemIndex, bool isBottom)
    {
        xlngInteractor.ItemsChanged(itemIndex, isBottom);
    }
}
