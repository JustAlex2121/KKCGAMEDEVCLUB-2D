using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CardClasses.Card;

namespace CardClasses
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]

    public class Card : ScriptableObject
    {

        public string cardName;

        public List<CardFlavor> cardFlavor;
        public int flavorPoints;

        public int damageMin;

        public int damageMax;

        public List<DamageType> damageType;



        public Sprite cardSprite;

        public void CardEffect()
        {

        }
       
        public enum CardFlavor
        {
            Salty,
            
            Sweet,
            
            Savory,
        }

        public enum DamageType
        {
            Salty,

            Sweet,

            Savory,
        }

        public enum weakness
        {
            Salty,

            Sweet,

            Savory,
        }

        public enum resistance
        {
            Salty,

            Sweet,

            Savory,
        }
    }


}

