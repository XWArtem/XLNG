using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = null;

    private void Awake()
    {
        if (gameManager == null && GameObject.Find("GameManager")) 
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }

    private void Update()
    {
        ResetAllStatsInput();
        AddBluePants();
        AddDarkBluePants();
        AddGreenCostume();
    }

    private void ResetAllStatsInput()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            gameManager.ResetAllStats();
        }
    }
    private void AddBluePants()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            gameManager.AddItem(1, "BluePants");
        }
    }

    private void AddDarkBluePants()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            gameManager.AddItem(2, "DarkBluePants");
        }
    }

    private void AddGreenCostume()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            gameManager.AddItem(3, "GreenCostume");
            gameManager.AddItem(4, "IceHat");
        }
    }

}
