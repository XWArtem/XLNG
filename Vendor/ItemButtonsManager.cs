using UnityEngine.UI;
using UnityEngine;

public class ItemButtonsManager : MonoBehaviour
{
    [SerializeField] private Button[] itemButtons;

    public delegate bool IsEnoughCoins(int itemCost);
    public IsEnoughCoins isEnoughCoins;
    public void InitializeButtons(int buttonsAmount)
    {
        itemButtons = new Button[buttonsAmount];
    }

    public void AddButton
        (int btnIndex, 
        Button btnSpawned, 
        string btnName, 
        int itemIndex, 
        bool itemAvailable, 
        bool isBottom, 
        int cost)
    {
        itemButtons[btnIndex] = btnSpawned;
        itemButtons[btnIndex].name = btnName;
        itemButtons[btnIndex].onClick.AddListener
            (() => { Buying(itemIndex, itemAvailable, isBottom, cost); }) ;
    }


    public void Buying(int itemIndex, bool itemAvailable, bool isBottom, int cost)
    {
        Debug.Log("123");
        // проверка на авэйлабл
        if (!itemAvailable)
        {
            // проверка на монеты
            if (isEnoughCoins?.Invoke(cost) == true)
            {
                // покупка
                Debug.Log("Item bought");
            }
            else
            {
                Debug.Log("Not enough coins");
            }
        }
    }
}
