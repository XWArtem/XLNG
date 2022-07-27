using UnityEngine;

public class LevelLoadingCanvasSpawner : MonoBehaviour
{
    [SerializeField] private GameObject levelLoadingCanvasPrefab;
    private Vector3 startPosition = Vector3.zero;

    private void Awake()
    {
        GameObject loadingCanvas = Instantiate(levelLoadingCanvasPrefab, startPosition, Quaternion.identity);
        loadingCanvas.name = "LevelLoadingCanvas";
        loadingCanvas.SetActive(false);
    }
}
