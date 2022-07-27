using UnityEngine;
using System;
using System.Collections.Generic;

public class ClothChanger : MonoBehaviour
{
    //[SerializeField] private MainHero mainHero;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject leftLeg;
    [SerializeField] private GameObject rightLeg;
    [SerializeField] private GameObject head;

    [SerializeField] SpriteRenderer bodySpriteRenderer = null;
    [SerializeField] SpriteRenderer leftLegSpriteRenderer = null;
    [SerializeField] SpriteRenderer rightLegSpriteRenderer = null;
    [SerializeField] SpriteRenderer headSpriteRenderer = null;

    private DataArchitecture.ClothRepository clothRepository;
    private DataArchitecture.ClothInteractor clothInteractor;

    // values from ClothRepo:
    public int CurrentItemTop { get; private set; }
    public int CurrentItemBottom { get; private set; }
    public int[] itemsAvailableIndexArr { get; private set; }
    public string[] itemsAvailableNameArr { get; private set; }
    public List<Tuple<int, string, int, bool>> itemsNamesList;

    [SerializeField] private InputController _inputController;

    private void Awake()
    {
        clothRepository = new DataArchitecture.ClothRepository();
        clothRepository.Initialize();
        clothInteractor = new DataArchitecture.ClothInteractor(clothRepository);

        GetClothRepoValues();

        // set up the cloth according to data from ClothRepo
        if (body && leftLeg && rightLeg && head)
        {
            bodySpriteRenderer = body.GetComponent<SpriteRenderer>();
            leftLegSpriteRenderer = leftLeg.GetComponent<SpriteRenderer>();
            rightLegSpriteRenderer = rightLeg.GetComponent<SpriteRenderer>();
            headSpriteRenderer = head.GetComponent<SpriteRenderer>();

            ChangeItem(CurrentItemTop, false);
            ChangeItem(CurrentItemBottom, true);
        }
        else Debug.Log("body, leftLeg, rightLeg or head is missing!");

        clothInteractor.ReadItemsAvailable();
    }

    private void Start()
    {
        _inputController.addItemDelegate += AddItem;
    }

    public void ChangeItem(int itemIndex, bool isBottom)
    {
        if (isBottom)  // change bottom
        {
            bodySpriteRenderer.sprite =
               Resources.Load<Sprite>(clothInteractor.GetBodyPartName("Body", itemIndex));
            leftLegSpriteRenderer.sprite =
                Resources.Load<Sprite>(clothInteractor.GetBodyPartName("LeftLeg", itemIndex));
            rightLegSpriteRenderer.sprite =
                Resources.Load<Sprite>(clothInteractor.GetBodyPartName("RightLeg", itemIndex));
        }
        else // change top
        {
            headSpriteRenderer.sprite =
                Resources.Load<Sprite>(clothInteractor.GetBodyPartName("Head", itemIndex));
        }
        clothInteractor.ItemsChanged(itemIndex, isBottom);
    }

    public void AddItem(int itemIndex, string itemName)
    {
        // first, check if the item already exists in our itemsAvailable List
        if (clothRepository.itemsAvailableIndexArr != null && Array.Exists(clothRepository.itemsAvailableIndexArr, x => x == itemIndex))
        {
            Debug.Log("Item already exists in the List of ItemsAvailable");
        }
        // if everything's fine and it doesn't exist yet. Add it and Save in Repo:
        else
        {
            clothInteractor.AddItem(itemIndex, itemName);
            GetClothRepoValues();
        }
    }
    public void ResetAllStats()
    {
        clothInteractor.ResetAllStats();
        GetClothRepoValues();
    }
    private void GetClothRepoValues()
    {
        // get values again from ClothRepo
        CurrentItemTop = clothRepository.CurrentItemTop;
        CurrentItemBottom = clothRepository.CurrentItemBottom;
        itemsAvailableIndexArr = clothRepository.itemsAvailableIndexArr;
        itemsAvailableNameArr = clothRepository.itemsAvailableNameArr;
        itemsNamesList = clothRepository.itemsNamesList;
    }

}
