using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomCardValue : MonoBehaviour
{
    [SerializeField] private DeckController deckController;
    [SerializeField] private Button randomButton;

    private int cardIndex = 0;
    
    private void Awake()
    {
        randomButton.onClick.RemoveAllListeners();
        randomButton.onClick.AddListener(RandomValue);
    }

    private void RandomValue()
    {
        var cards = deckController.GetDeckCards();
        
        if(cards.Count == 0)
            return;

        
        if (cardIndex >= cards.Count)
        {
            cardIndex = 0;
        }
        
        var rValue = Random.Range(0, 3);
        
        switch (rValue)
        {
            case 0:
                cards[cardIndex].dataModel.hp = Random.Range(0, 10);
                break;
            case 1:
                cards[cardIndex].dataModel.mana = Random.Range(0, 10);
                break;
            case 2:
                cards[cardIndex].dataModel.attack = Random.Range(0, 10);
                break;
        }

        cards[cardIndex].Setup(cards[cardIndex].dataModel);
        if (cards[cardIndex].dataModel.hp <= 0)
        {
            deckController.DestroyCard(cards[cardIndex]);
        }
        
        
        cardIndex++;
    }
}
