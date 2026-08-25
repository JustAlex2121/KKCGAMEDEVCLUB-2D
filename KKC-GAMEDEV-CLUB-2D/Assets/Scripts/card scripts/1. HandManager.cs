using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardClasses;

public class HandManager : MonoBehaviour
{

   public DeckManager DeckManager;
   public List<GameObject> cardPrefabs; //Assign card prefab in inspector

    public Transform handTransform; //hand position center
    public GameObject Handposition; // the object that gets shown/hidden

    public float handSpread = -7.5f; //how much hand is spread out
  
    public List<GameObject> cardsInHand = new List<GameObject>(); //hold a list of the card objects in player hand

    public float cardSpacing = 150f;

    public float verticalSpacing = 100f;

    public int cardsPerTurn = 6; // Number of cards to draw each turn (set default or adjust as needed)

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState state)
    {
        if (state == GameState.PlayerTurn)
        {
            StartNewPlayerTurn();
        }
    }

    private void StartNewPlayerTurn()
    {
        Handposition.SetActive(true);

        foreach (GameObject card in cardsInHand)
        {
            Destroy(card);
        }
        cardsInHand.Clear();

        for (int i = 0; i < cardsPerTurn; i++)
        {
            DeckManager.DrawCard(this);
        }
    }
    public List<GameObject> GetCardsInHand()
    {
        return cardsInHand;
    }

    public void AddCardToHand(Card cardData)
    {
        if (cardPrefabs == null || cardPrefabs.Count == 0)
        {
            Debug.LogError("Card prefabs not assigned in HandManager.");
            return;
        }
        
        //Spawn the card
        Debug.Log("adding card: " + cardData.cardName);
        GameObject matchingPrefab = cardPrefabs[0];
        foreach (GameObject prefab in cardPrefabs)
        {
            CardDisplay display = prefab.GetComponent<CardDisplay>();
            Debug.Log("checking prefab: " + prefab.name + "card:" + (display?.cardData?.cardName ?? "null"));
            if (display != null && display.cardData == cardData)
            {
                matchingPrefab = prefab;
                break;
            }
        }
        GameObject newCard = Instantiate(matchingPrefab, handTransform.position, Quaternion.identity, handTransform);

        //set the card data of the spawned card
        newCard.GetComponent<CardDisplay>().SetCardData(cardData);
        cardsInHand.Add(newCard);
        Debug.Log(cardsInHand[0]);
        UpdateHandVisuals();
    }

    private void UpdateHandVisuals() 
    {
        int cardCount = cardsInHand.Count;

        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
        }


        for (int i = 0; i < cardCount; i++)
        {
            float rotationAngle = (handSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));

            float normalizedPosition = cardCount > 1 ? (2f * i / (cardCount - 1) - 1f) : 0f; //Normalize hand position btw -1, 1
            float verticalOffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition);

            //set Card positions
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        }
    }

    //may need for debugging
    void Start() //Card in the deck
    {
       // AddCardToHand();
      //  AddCardToHand();
      //  AddCardToHand();
    }
    
   /*  public void AddCardToHand() //Can be updated in the engine editor 
    {
        //Instaniate the card
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
    } */
}