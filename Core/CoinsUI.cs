using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUI : MonoBehaviour
{
    public TextMeshProUGUI _coinsRender;
    private int coins;


    public int Coins
    {
        get 
        { 
            return coins; 
        }
        set 
        { 
            coins = value;
            _coinsRender.text = coins.ToString();
        }
    }
}
