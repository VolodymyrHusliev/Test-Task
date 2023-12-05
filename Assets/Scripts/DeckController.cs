using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class DeckController : MonoBehaviour
{
    [FormerlySerializedAs("cardPrefab")] [SerializeField] private UICard uiCardPrefab;
    [SerializeField] private RectTransform root;
    [SerializeField] private List<UICard> instancedCards;
    [SerializeField] private List<UICard> cardsOnPlayArea;

    public void SetupDeck(List<CardModel> cards)
    {
        if(instancedCards != null)
            instancedCards.ForEach(x => Destroy(x.gameObject));
        instancedCards = new List<UICard>();

        for (var i = 0; i < cards.Count; i++)
        {
            var item = Instantiate(uiCardPrefab, root);
            item.Setup(cards[i]);
            instancedCards.Add(item);
        }
    }

    public void DropCardOnPlayArea(UICard card)
    {
        cardsOnPlayArea.Clear();
        cardsOnPlayArea.Add(card);
    }

    public List<UICard> GetDeckCards()
    {
        return instancedCards.Except(cardsOnPlayArea).ToList();
    }

    public void DestroyCard(UICard card)
    {
        instancedCards.Remove(card);
        Destroy(card.gameObject);
    }
}
