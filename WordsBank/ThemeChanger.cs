using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ThemeChanger : MonoBehaviour
{
    [SerializeField] private Button[] themeButtons = new Button[15];
    private int numberOfThemeButtons;

    [SerializeField] private GameObject[] wordsContent = new GameObject[15];
    
    private string[] wordsContentNames = 
    { 
        "AllWordsContent", 
        "BasicsContent", 
        "CaraContent", 
        "CuerpoContent", 
        "SobreTiempoContent", 
        "ColoresContent",
        "NumericosContent",
        "ComidaParte1Content",
        "RopaContent",
        "CasaContent",
        "TransporteContent",
        "ProfesionesContent",
        "FamiliaContent",
        "VidaCotidianaContent",
        "CuestionesContent"
    };

    private ThemeSpawner themeSpawner;
    private ScrollRect wordsScrollRect = null;
    private Scrollbar wordsScrollbar = null;
    private Scrollbar themeScrollbar = null;
    private ColorBlock themeButtonColor;
    private int previousPressedThemeButtonIndex = 99;

    // working in Start because we need Awake of ThemeSpawner first
    private void Start()
    {
        themeSpawner = FindObjectOfType<ThemeSpawner>();
        numberOfThemeButtons = themeSpawner.themesAmount;
        FindButtons();
        FindWordContent();
        // add listener to all our buttons using index to know which button was clicked then
        for (int i = 0; i < numberOfThemeButtons; i++)
        {
            int index = i;
            themeButtons[index].onClick.AddListener(() => ChangeTheme(index));
        }
        // find ScrollRect to set Content later
        if (wordsScrollRect == null)
        {
            wordsScrollRect = GameObject.Find("WordsScrollRect").GetComponent<ScrollRect>();
        }
        // find ScrollBar
        if (wordsScrollbar == null)
        {
            wordsScrollbar = GameObject.Find("WordsScrollbar").GetComponent<Scrollbar>();
        }
        // find ThemeScrollbar and set Value to 1 then
        if (themeScrollbar == null)
        {
            themeScrollbar = GameObject.Find("ThemeScrollbar").GetComponent<Scrollbar>();
            themeScrollbar.value = 1;
        }

        
    }

    private void FindButtons()
    {
        for (int i = 0; i < numberOfThemeButtons; i++)
        {
            themeButtons[i] = GameObject.Find("ThemeButton" + i.ToString()).GetComponent<Button>();
        }
    }

    private void FindWordContent()
    {
        for (int i = 0; i < wordsContent.Length; i++)
        {
            wordsContent[i] = GameObject.Find(wordsContentNames[i]);
            wordsContent[i].SetActive(false);
        }
    }

    public void ChangeTheme(int themeIndex)
    {
        // first, disable all other Contents
        for (int i = 0; i < wordsContent.Length; i++)
        {
            wordsContent[i].SetActive(false);
        }
        // if some themeButton was pressed previously, change it's color
        
          
         if (previousPressedThemeButtonIndex != 99)
        {
            themeButtonColor = themeButtons[themeIndex].GetComponent<Button>().colors;
            themeButtonColor.normalColor = new Color32(255, 255, 255, 255);
            themeButtons[previousPressedThemeButtonIndex].colors = themeButtonColor;
        }
        // remember the index of pressed button to change its' color to White in the future
        previousPressedThemeButtonIndex = themeIndex;

        

        // change pressed button color - set it green 
        themeButtonColor = themeButtons[themeIndex].GetComponent<Button>().colors;
        themeButtonColor.normalColor = new Color32(175,255,88,255);
        themeButtons[themeIndex].colors = themeButtonColor;

        // enable our Content
        wordsContent[themeIndex].SetActive(true);

        // change Content in Scroll Rect to be able to scroll it
        wordsScrollRect.content = wordsContent[themeIndex].GetComponent<RectTransform>();
        // set scrollbar value to 0
        wordsScrollbar.value = 1;
    }


}
