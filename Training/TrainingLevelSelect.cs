using UnityEngine;
using TMPro;

public class TrainingLevelSelect : MonoBehaviour
{
    [SerializeField] private GameObject levelButton;
    private TMP_Text buttonText;

    private GameNavigation gameNavigation = null;

    private int levelsAmount;

    private void Start()
    {
        levelsAmount = 10;
        SpawnLevelButtons(levelsAmount);
        if (FindObjectOfType<GameNavigation>())
        {
            gameNavigation = FindObjectOfType<GameNavigation>();
            gameNavigation.FindAllButtonsOnScene();
        }
    }

    private void SpawnLevelButtons(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            buttonText = levelButton.GetComponentInChildren<TMP_Text>();
            buttonText.text = i.ToString();
            Instantiate(levelButton, new Vector3(0f, 0f, 0f), Quaternion.identity,
                GameObject.FindGameObjectWithTag("LevelButtonsContent").transform);
        }
    }

}
