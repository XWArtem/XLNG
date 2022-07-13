using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameNavigation : MonoBehaviour
{
   // public static GameNavigation instance;

    [SerializeField] private Button wordsBankBtn = null;
    [SerializeField] private Button exitBtn = null;
    [SerializeField] private Button settingsBtn = null;
    [SerializeField] private Button backBtn = null;
    [SerializeField] private Button trainingBtn = null;
    [SerializeField] private Button vendorBtn = null;
    [SerializeField] private Button heroBtn = null;

    private GameObject mainMenuCanvas = null;
    private GameObject _levelLoadingCanvas = null;
    private Slider _levelLoadingSlider = null;

    private GameObject myHero = null;

    private Animator animator;

    private void Awake()
    {
        /*
        if (mainCamera == null && GameObject.Find("MainCamera"))
        {
            mainCamera = GameObject.Find("UICameraMainMenu").GetComponent<Camera>();
            Debug.Log("MainCamera is: " + mainCamera);
        }
        if (UICameraTraining == null && GameObject.Find("UICameraTraining"))
        {
            UICameraTraining = GameObject.Find("UICameraTraining").GetComponent<Camera>();
            Debug.Log("UICameraTraining is: " + UICameraTraining);
        }
        */
        if (myHero == null)
        {
            myHero = GameObject.Find("MyHero");
        }

        if (animator == null)
        {
            animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        }
    }

    private void Start()
    {
        if (_levelLoadingSlider == null && GameObject.Find("LevelLoadingSlider"))
        {
            _levelLoadingSlider = GameObject.Find("LevelLoadingSlider").GetComponent<Slider>();
        }
        FindAllButtonsOnScene();
        FindAllCanvasesOnScene();
 
        //DontDestroyOnLoad(gameObject);
    }
    public void FindAllButtonsOnScene()
    {
        if (wordsBankBtn == null && GameObject.Find("Button_WordsBank"))
        {
            wordsBankBtn = GameObject.Find("Button_WordsBank").GetComponent<Button>();
            wordsBankBtn.onClick.AddListener(WordBankClicked);
        }
        if (exitBtn == null && GameObject.Find("Button_Exit"))
        {
            exitBtn = GameObject.Find("Button_Exit").GetComponent<Button>();
            exitBtn.onClick.AddListener(ExitClicked);
        }
        if (settingsBtn == null && GameObject.Find("Button_Settings"))
        {
            settingsBtn = GameObject.Find("Button_Settings").GetComponent<Button>();
            settingsBtn.onClick.AddListener(SettingsClicked);
        }
        if (backBtn == null && GameObject.Find("Button_Back"))
        {
            backBtn = GameObject.Find("Button_Back").GetComponent<Button>();
            backBtn.onClick.AddListener(BackClicked);
        }
        if (trainingBtn == null && GameObject.Find("Button_Training"))
        {
            trainingBtn = GameObject.Find("Button_Training").GetComponent<Button>();
            trainingBtn.onClick.AddListener(TrainingClicked);
        }
        if (vendorBtn == null && GameObject.Find("Button_Vendor"))
        {
            vendorBtn = GameObject.Find("Button_Vendor").GetComponent<Button>();
            vendorBtn.onClick.AddListener(VendorClicked);
        }
        if (heroBtn == null && GameObject.Find("Button_Hero"))
        {
            heroBtn = GameObject.Find("Button_Hero").GetComponent<Button>();
            heroBtn.onClick.AddListener(HeroClicked);
        }
    }

    private void FindAllCanvasesOnScene()
    {
        if (mainMenuCanvas == null && GameObject.Find("MainMenuCanvas"))
        {
            mainMenuCanvas = GameObject.Find("MainMenuCanvas");
        }
        if (_levelLoadingCanvas == null && GameObject.Find("LevelLoadingCanvas"))
        {
            _levelLoadingCanvas = GameObject.Find("LevelLoadingCanvas");
            // disable that Canvas on Start
            _levelLoadingCanvas.SetActive(false);
        }
    }

    public void AddAllListeners()
    {
        if (wordsBankBtn != null) wordsBankBtn.onClick.AddListener(WordBankClicked);
        if (exitBtn != null) exitBtn.onClick.AddListener(ExitClicked);
        if (settingsBtn != null) settingsBtn.onClick.AddListener(SettingsClicked);
        if (backBtn != null) backBtn.onClick.AddListener(BackClicked);
        if (trainingBtn) trainingBtn.onClick.AddListener(TrainingClicked);
        if (vendorBtn) vendorBtn.onClick.AddListener(VendorClicked);
        if (heroBtn) heroBtn.onClick.AddListener(HeroClicked);
    }

    private void OnEnable()
    {
       AddAllListeners();
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
            //SceneManager.LoadScene("WordsBank", LoadSceneMode.Additive);
            StartCoroutine(LoadAsynchronously(1, true));
            ManageCanvases(1);
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
            //SceneManager.LoadScene("MainMenu",LoadSceneMode.Single);
            StartCoroutine(LoadAsynchronously(0, false));
            FindAllButtonsOnScene();
            ManageCanvases(0);
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
            ManageCanvases(2);
            myHero.GetComponent<MainHero>().ChangeHeroPosition(2);
        }
        
    }
    // Vendor scene == scene 3
    private void VendorClicked()
    {
        if (SceneManager.GetSceneByName("Vendor") != null)
        {
            StartCoroutine(LoadAsynchronously(3, true));
            ManageCanvases(3);
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
            ManageCanvases(4);
            myHero.GetComponent<MainHero>().ChangeHeroPosition(4);
        }
    }

    // this methods should run each time scenes change
    private void ManageCanvases(int loadingSceneIndex)
    {
        if (loadingSceneIndex != 0 && mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(false);
        }
        else if (loadingSceneIndex == 0 && mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(true);
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
            _levelLoadingCanvas.SetActive(true);

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
            _levelLoadingCanvas.SetActive(true);

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
            Debug.Log("SceneIndex is: " + animator.GetInteger("SceneIndex"));
        }
        else if (sceneIndex == 2 && animator)
        {
            // set Animation for Training Idle
            animator.SetInteger("SceneIndex", 2);
            Debug.Log("SceneIndex is: " + animator.GetInteger("SceneIndex"));
        }
        

        // disable loading Canvas
        _levelLoadingCanvas.SetActive(false);
        FindAllButtonsOnScene();


    }

    private void FullCheckBeforeSceneLoading(int sceneIndex)
    {


    }

}
