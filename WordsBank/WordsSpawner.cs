using UnityEngine;
using TMPro;

public class WordsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;
    private int tableSize;
    private int tablesAmount;

    private TMP_Text buttonText;

    private CSVReader csvReader;
    private GameNavigation gameNavigation = null;

    void Start()
    {
        csvReader = FindObjectOfType<CSVReader>();
        
        tablesAmount = csvReader.TablesAmount;
        for (int i = 0; i < tablesAmount; i++)
        {
            // get tableSize from CSV reader for each table we initialize
            tableSize = csvReader.tableSize[i+1];
            // and initialize, getting the name of that table
            SpawnButtons(tableSize, csvReader.TableName[i+1]);
        }
        if (FindObjectOfType<GameNavigation>())
        {
            gameNavigation = FindObjectOfType<GameNavigation>();
            // we need it here to find buttons on Start function after gameNavigation is found:
            gameNavigation.FindAllButtonsOnScene();
        }
    }

    public void SpawnButtons(int amount, string tag)
    {
        for (int i = 0; i < amount; i++)
        {
            buttonText = leftButton.GetComponentInChildren<TMP_Text>();
            buttonText.text = csvReader.SetRusWord(i, tag);
            Instantiate(leftButton, new Vector3(0f, 0f, 0f), Quaternion.identity,
                GameObject.FindGameObjectWithTag(tag).transform);

            
            buttonText = rightButton.GetComponentInChildren<TMP_Text>();
            buttonText.text = csvReader.SetEspWord(i, tag);
            Instantiate(rightButton, new Vector3(0f, 0f, 0f), Quaternion.identity,
                GameObject.FindGameObjectWithTag(tag).transform);
        }
    }

}
