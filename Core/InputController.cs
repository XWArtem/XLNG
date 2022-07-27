using UnityEngine;


public class InputController : MonoBehaviour
{
    [SerializeField] private ClothChanger _clothChanger;

    public delegate void AddItemToClothManager(int itemIndex, string itemName);
    public AddItemToClothManager addItemDelegate;

    public delegate void AddCoins(object sender, int value);
    public AddCoins AddCoinsDelegate;

    [SerializeField] SceneConfig _sceneConfig;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            addItemDelegate?.Invoke(1, "BluePants");
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            _sceneConfig.AddCoinsTest(10);
        }
        ResetAllStatsInput();
        AddDarkBluePants();
        AddGreenCostume();
    }

    private void ResetAllStatsInput()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            _clothChanger.ResetAllStats();

        }
    }



    private void AddDarkBluePants()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            _clothChanger.AddItem(2, "DarkBluePants");
        }
    }

    private void AddGreenCostume()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            _clothChanger.AddItem(3, "GreenCostume");
            _clothChanger.AddItem(4, "IceHat");
        }
    }
}
