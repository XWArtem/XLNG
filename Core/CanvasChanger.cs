using UnityEngine;

public class CanvasChanger : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject _levelLoadingCanvas;
    [SerializeField] private GameNavigation gameNavigation;

    private void Start()
    {
        FindAllCanvasesOnScene();
        SetLoadingCanvas(false);
    }

    private void OnEnable()
    {
        gameNavigation.ManageSceneCanvases += SetSceneCanvases;
        gameNavigation.ManageLoadingCanvas += SetLoadingCanvas;
    }

    private void OnDisable()
    {
        gameNavigation.ManageSceneCanvases -= SetSceneCanvases;
        gameNavigation.ManageLoadingCanvas -= SetLoadingCanvas;
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
        }
    }

    private void SetSceneCanvases(int sceneIndex)
    {
        if (sceneIndex != 0 && mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(false);
        }
        else if (sceneIndex == 0 && mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(true);
        }
    }
    private void SetLoadingCanvas(bool isActive)
    {
        _levelLoadingCanvas.SetActive(isActive);
    }
}
