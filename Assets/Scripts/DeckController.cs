using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UI;

namespace Card
{
    public class DeckController : MonoBehaviour
    {
        [SerializeField] private UICard uiCardPrefab;
        [SerializeField] private RectTransform root;
        [SerializeField] private List<UICard> instancedCards;
        [SerializeField] private List<UICard> cardsOnPlayArea;

        internal void SetupDeck(List<CardModel> cards)
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
        internal List<UICard> GetDeckCards()
        {
            return instancedCards.Except(cardsOnPlayArea).ToList();
        }

        internal void DestroyCard(UICard card)
        {
            instancedCards.Remove(card);
            Destroy(card.gameObject);
        }
        public void DropCardOnPlayArea(UICard card)
        {
            cardsOnPlayArea.Clear();
            cardsOnPlayArea.Add(card);
        }
    }
}
