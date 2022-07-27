using UnityEngine;

public class SceneConfig : MonoBehaviour
{
    private DataArchitecture.XLNGInteractor _xlngInteractor;
    private DataArchitecture.XLNGRepository _xlngRepository;
    [SerializeField] InputController _inputController;
    [SerializeField] CoinsUI _coinsUI;

    private void Awake()
    {
        _xlngRepository = new DataArchitecture.XLNGRepository();
        _xlngRepository.Initialize();
        _xlngInteractor = new DataArchitecture.XLNGInteractor(_xlngRepository);
        _coinsUI.Coins = _xlngInteractor.Coins;
        Debug.Log("Something for logger testing");
    }

    private void OnEnable()
    {
        _inputController.AddCoinsDelegate += _xlngInteractor.AddCoins;
        _xlngInteractor.OnCoinsAdded += CoinsUpdate;
    }
    private void OnDisable()
    {
        _inputController.AddCoinsDelegate -= _xlngInteractor.AddCoins;
        _xlngInteractor.OnCoinsAdded -= CoinsUpdate;
    }

    public void CoinsUpdate(int value)
    {
        _coinsUI.Coins += value;
    }

    public void AddCoinsTest(int value)
    {
       _xlngInteractor.AddCoins(this, value);
    }
}
