using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Cloth Manager -> GameManager -> XLNGInteractor -> XLNGRepo
/// </summary>

public class ClothManager : MonoBehaviour
{
    private GameManager gameManager;
    private MainHero mainHero;
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

    public List<Tuple<int, string, int, bool>> itemsNamesList;

    private void Awake()
    {
        //find gameManager
        if (!gameManager && GameObject.Find("GameManager"))
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        // find the hero itself
        if (!mainHero && GameObject.Find("MyHero"))
        {
            mainHero = GameObject.Find("MyHero").GetComponent<MainHero>();
        }

        // find it's body parts
        body = GameObject.Find("Body");
        leftLeg = GameObject.Find("LeftLeg");
        rightLeg = GameObject.Find("RightLeg");
        head = GameObject.Find("Head");

        // set up the cloth according to data from Repo
        if (body != null && leftLeg != null && rightLeg != null && head != null)
        {
            bodySpriteRenderer = body.GetComponent<SpriteRenderer>();
            bodySpriteRenderer.sprite = Resources.Load<Sprite>("BodyNoTexture");


            leftLegSpriteRenderer = leftLeg.GetComponent<SpriteRenderer>();
            leftLegSpriteRenderer.sprite = Resources.Load<Sprite>("LeftLegNoTexture");

            rightLegSpriteRenderer = rightLeg.GetComponent<SpriteRenderer>();
            rightLegSpriteRenderer.sprite = Resources.Load<Sprite>("RightLegNoTexture");

            headSpriteRenderer = head.GetComponent<SpriteRenderer>();
            headSpriteRenderer.sprite = Resources.Load<Sprite>("HeadNoTexture");
        }
        else Debug.Log("body, leftLeg, rightLeg or head is missing!");

        // init ClothRepo and Interactor
        clothRepository = new DataArchitecture.ClothRepository();
        clothRepository.Initialize();
        clothInteractor = new DataArchitecture.ClothInteractor(clothRepository);

        itemsNamesList = clothRepository.itemsNamesList;
    }

    public void ChangeItem(int itemIndex, bool isBottom)
    {
        // check if item is available. LATER

        
        if (isBottom)  // change bottom
        {
            bodySpriteRenderer.sprite = 
               Resources.Load<Sprite>(clothInteractor.BodyChange(itemIndex));

            leftLegSpriteRenderer.sprite = 
                Resources.Load<Sprite>(clothInteractor.LeftLegChange(itemIndex));

            rightLegSpriteRenderer.sprite = 
                Resources.Load<Sprite>(clothInteractor.RightLegChange(itemIndex));

            // save changes to XLNGRepo via GameManager
            
        }
        else // change top
        {
            headSpriteRenderer.sprite =
                Resources.Load<Sprite>(clothInteractor.HeadChange(itemIndex));

        }
        gameManager.ItemsChanged(itemIndex, isBottom);
    }

}
