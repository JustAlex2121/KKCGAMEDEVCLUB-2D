using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardClasses;

public class DeckManager : MonoBehaviour
{
  public List<Card> allCards = new List<Card>();
    //put cards in Resources Folder not CardData
  //private int currentIndex = 0;

    void Start()
    {
        HandManager hand = FindFirstObjectByType<HandManager>();

        
        hand.cardsInHand.Clear();


        //Load all cards from the resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");
        Debug.Log("cards");

        //Add the loaded cards to the allcards list
        allCards.Clear();
        allCards.AddRange(cards);

        // ShuffleDeck();
        Debug.Log("Loaded " + allCards.Count + " cards into deck.");
        //currentIndex = 0;

        for (int i = 0; i < 6; i++){
            DrawCard(hand);
        }
    }
     public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0)
        {
            Debug.Log("Deck is empty — no card drawn.");
            return;
        }

        int randomIndex = Random.Range(0, allCards.Count);
        Card nextCard = allCards[randomIndex];
        handManager.AddCardToHand(nextCard);
    }
    //private void ShuffleDeck()
    //{
        //for (int i = 0; i < allCards.Count; i++)
        //{
            //Card temp = allCards[i];
            //int randomIndex = Random.Range(0, allCards.Count);
            //allCards[i] = allCards[randomIndex];
           // allCards[randomIndex] = temp;
       // }
    //}

    

       /* HandManager hand = FindFirstObjectByType<HandManager>();
        for (int i = 0; i < 6; i++)
        {
            DrawCard(hand);
        } */
    
}
