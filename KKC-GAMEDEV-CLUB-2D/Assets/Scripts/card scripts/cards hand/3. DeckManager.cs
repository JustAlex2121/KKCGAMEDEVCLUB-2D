using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardClasses;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();
    //put cards in Resources Folder not CardData

    void Start()
    {
        // Load all cards from the Resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");
        allCards.Clear();
        allCards.AddRange(cards);
        Debug.Log("Loaded " + allCards.Count + " cards into deck.");

        // Drawing the first hand is now handled by HandManager
        // when the game transitions into GameState.PlayerTurn for the first time.
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
}
