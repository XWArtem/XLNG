using UnityEngine;

public class MainHero : MonoBehaviour
{
    public void ChangeHeroPosition(int SceneIndex)
    {
        if (SceneIndex == 3)  // Vendor Scene 
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(240, -80);
        }
        if (SceneIndex == 0 || SceneIndex == 2) // Main Menu Scene or Training Scene
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
        if (SceneIndex == 4) // HeroMenu
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(360, -60);
            GetComponent<RectTransform>().localScale = new Vector2(60f, 60f);
        }
    }
}
