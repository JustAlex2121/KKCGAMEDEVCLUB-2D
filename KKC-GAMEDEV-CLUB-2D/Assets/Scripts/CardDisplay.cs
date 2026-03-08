using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CardClasses;

public class CardDisplay : MonoBehaviour
{
    public Image cardImage;

    public TMP_Text nameText;

    public TMP_Text flavorPointsText;

    public TMP_Text damageText;

    public Image[] typeImages;
    //Packages.Card cardData;
    public Card cardData;

    private Color[] cardColors =
    {
        Color.red,    //Spicy
        Color.yellow, //Salty
        Color.blue    //Sweet
        
    };

    private Color[] typeColors =
   {
        Color.magenta,  //Spicy
        Color.black,    //Salty
        Color.white    //Sweet
        
    };

    void Start()
    {
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {
        //Update Card Image color based on Damage Type
        cardImage.color = cardColors[(int)cardData.cardFlavor[0]];

        nameText.text = cardData.cardName;
        flavorPointsText.text = cardData.flavorPoints.ToString();
        damageText.text = $"{cardData.damageMin} - {cardData.damageMax}";

        //Update Card Type Image
        for (int i = 0; i < typeImages.Length; i++)
        {
            if (i < cardData.cardFlavor.Count)
            {
                typeImages[i].gameObject.SetActive(true);
                typeImages[i].color = typeColors[(int)cardData.cardFlavor[i]];
            }
            else
            {
                typeImages[i].gameObject.SetActive(false);
            }
        }
    }
}
