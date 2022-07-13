using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class ThemeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject themeSelectButton;
    private TMP_Text themeSelectButtonText;

    private GameNavigation gameNavigation = null;
    public int themesAmount;
    // 15 elements for that list:
    private List<string> themeList = new List<string> {
        "Все", 
        "Базовые слова", 
        "Лицо", 
        "Тело", 
        "О времени", 
        "Цвета", 
        "Цифры и числа", 
        "Еда (ч.1)", 
        "Одежда",
        "Дом",
        "Транспорт",
        "Профессии",
        "Семья",
        "Распорядок дня",
        "Вопросительные слова"
    };


    private void Awake()
    {
        themesAmount = themeList.Count;
        Debug.Log("themesAmount is:" + themesAmount);
        SpawnLevelButtons(themesAmount);
        if (FindObjectOfType<GameNavigation>())
        {
            gameNavigation = FindObjectOfType<GameNavigation>();
            gameNavigation.FindAllButtonsOnScene();
        }
    }

    private void SpawnLevelButtons(int buttonsAmount)
    {
        for (int i = 0; i < buttonsAmount; i++)
        {
            themeSelectButtonText = themeSelectButton.GetComponentInChildren<TMP_Text>();
            themeSelectButtonText.text = themeList[i].ToString();
            GameObject newButton = Instantiate(themeSelectButton, 
                new Vector3(0f, 0f, 0f), Quaternion.identity,
                GameObject.FindGameObjectWithTag("ThemeSelectContent").transform);
            newButton.name = "ThemeButton" + i.ToString();
        }
    }

    


}




