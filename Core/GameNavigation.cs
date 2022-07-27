using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

public class TupleList<T1, T2, T3> : List<Tuple<int, Button, UnityAction>>
{
    public void Add(int index, Button btn, UnityAction action)
    {
        Add(new Tuple<int, Button, UnityAction>(index, btn, action));
    }
}

public class GameNavigation : MonoBehaviour
{
    private delegate void AddListeners();

    [SerializeField] private Button[] allButtonsInGame;
    private Button wordsBankBtn;
    private Button exitBtn;
    private Button settingsBtn;
    private Button backBtn;
    private Button trainingBtn;
    private Button vendorBtn;
    private Button heroBtn;

    [SerializeField] private GameObject levelLoadingCanvas;
    private Slider _levelLoadingSlider = null;

    [SerializeField] private GameObject myHero = null;
    [SerializeField] private Animator animator;

    public delegate void setCanvas(int sceneIndex);
    public setCanvas ManageSceneCanvases;

    public delegate void setLoadingCanvas(bool isActive);
    public setLoadingCanvas ManageLoadingCanvas;
    TupleList<int, Button, Action> ButtonsAndActions;

    private void Awake()
    {
         ButtonsAndActions = new TupleList<int, Button, Action> 
            {
                {0, wordsBankBtn, WordBankClicked},
                {1, exitBtn,  ExitClicked},
                {2, settingsBtn, SettingsClicked},
                {3, backBtn, BackClicked},
                {4, trainingBtn, TrainingClicked},
                {5, vendorBtn, VendorClicked},
                {6, heroBtn, HeroClicked},
            };
    }

    private void Start()
    {
        FindAllButtonsOnScene();
        _levelLoadingSlider = levelLoadingCanvas.GetComponentInChildren<Slider>();
    }
    public void FindAllButtonsOnScene()
    {
        allButtonsInGame = new Button[] 
        {
            wordsBankBtn,
            exitBtn,
            settingsBtn,
            backBtn,
            trainingBtn,
            vendorBtn,
            heroBtn,
        };

        for (int i = 0; i < ButtonsAndActions.Count; i++)
        {
            if (allButtonsInGame[i] == null && GameObject.Find(Static_strings.BUTTONS_NAMES[i]))
            {
                allButtonsInGame[i] = GameObject.Find($"{Static_strings.BUTTONS_NAMES[i]}").GetComponent<Button>();
                var x = i;
                allButtonsInGame[x].onClick.AddListener(ButtonsAndActions[i].Item3);
            }
        }
    }

    private void OnDisable()
    {
        if (wordsBankBtn != null) wordsBankBtn.onClick.RemoveListener(WordBankClicked);
        if (exitBtn != null) exitBtn.onClick.RemoveListener(ExitClicked);
        if (settingsBtn != null) settingsBtn.onClick.RemoveListener(SettingsClicked);
        if (backBtn != null) backBtn.onClick.RemoveListener(BackClicked);
        if (trainingBtn) trainingBtn.onClick.RemoveListener(TrainingClicked);
        if (vendorBtn) vendorBtn.onClick.RemoveListener(VendorClicked);
        if (heroBtn) heroBtn.onClick.RemoveListener(HeroClicked);
    }


    // WordsBank scene == scene 1
    private void WordBankClicked()
    {
        if (SceneManager.GetSceneByName("WordsBank") != null)
        {
            StartCoroutine(LoadAsynchronously(1, true));
            ManageSceneCanvases?.Invoke(1);
        }
    }
    private void ExitClicked()
    {
        Application.Quit();
    }
    private void SettingsClicked()
    {
        Debug.Log("Go to settings");
    }

    // MainMenu scene == scene 0
    private void BackClicked()
    {
        if (SceneManager.GetSceneByName("MainMenu") != null)
        {
            StartCoroutine(LoadAsynchronously(0, false));
            FindAllButtonsOnScene();
            ManageSceneCanvases?.Invoke(0);
            myHero.GetComponent<MainHero>().ChangeHeroPosition(0);
        }
    }

    // Training scene == scene 2
    private void TrainingClicked()
    {
        if (SceneManager.GetSceneByName("Training") != null)
        {
            StartCoroutine(LoadAsynchronously(2, true));
            //SceneManager.LoadScene("Training",LoadSceneMode.Additive);
            ManageSceneCanvases?.Invoke(2);
            myHero.GetComponent<MainHero>().ChangeHeroPosition(2);
        }
        
    }

    // Vendor scene == scene 3
    private void VendorClicked()
    {
        if (SceneManager.GetSceneByName("Vendor") != null)
        {
            StartCoroutine(LoadAsynchronously(3, true));
            ManageSceneCanvases?.Invoke(3);
            // also change hero position
            myHero.GetComponent<MainHero>().ChangeHeroPosition(3);
        }
    }

    // HeroMenu scene == scene 4
    private void HeroClicked()
    {
        if (SceneManager.GetSceneByName("HeroMenu") != null)
        {
            StartCoroutine(LoadAsynchronously(4, true));
            ManageSceneCanvases?.Invoke(4);
            myHero.GetComponent<MainHero>().ChangeHeroPosition(4);
        }
    }

    IEnumerator LoadAsynchronously(int sceneIndex, bool loadSceneModeIsAddictive)
    {
        FullCheckBeforeSceneLoading(sceneIndex);
        // disable MainHero
        if (myHero)
        {
            myHero.SetActive(false);
        }

        if (loadSceneModeIsAddictive)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
            // enable loading Canvas
            ManageLoadingCanvas?.Invoke(true);

            while (!operation.isDone)
            {
                float _loadingProgress = Mathf.Clamp01(operation.progress / 0.9f);
                _levelLoadingSlider.value = _loadingProgress;
                yield return null;
            }
        }
        else
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
            // enable loading Canvas
            ManageLoadingCanvas?.Invoke(true);

            while (!operation.isDone)
            {
                float _loadingProgress = Mathf.Clamp01(operation.progress / 0.9f);
                _levelLoadingSlider.value = _loadingProgress;
                yield return null;
            }
        }

        // enable MainHero
        if (myHero)
        {
            myHero.SetActive(true);
        }


        if (sceneIndex == 0 && animator)
        {
            // set Animation for MainMenu Idle
            animator.SetInteger("SceneIndex", 0);
        }

        else if (sceneIndex == 2 && animator)
        {
            // set Animation for Training Idle
            animator.SetInteger("SceneIndex", 2);
        }
        // disable loading Canvas
        ManageLoadingCanvas?.Invoke(false);
        FindAllButtonsOnScene();
    }

    private void FullCheckBeforeSceneLoading(int sceneIndex)
    {

    }

}
