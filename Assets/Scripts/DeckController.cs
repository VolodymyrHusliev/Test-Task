using System.Collections.Generic;
using CardGame.Networking.Models;
using UnityEngine;


namespace CardGame.Core.UI
{
    public class DeckController : MonoBehaviour
    {
        [SerializeField] private UICard uiCardPrefab;
        [SerializeField] private RectTransform root;
        [SerializeField] private List<UICard> instancedCards;
        [SerializeField] private List<UICard> cardsOnPlayArea;

        public void SetupDeck(List<CardModel> cards)
        {
            if (instancedCards != null)
                instancedCards.ForEach(x => Destroy(x.gameObject));
            instancedCards = new List<UICard>();

            for (var i = 0; i < cards.Count; i++)
            {
                var item = Instantiate(uiCardPrefab, root);
                item.Setup(cards[i]);
                instancedCards.Add(item);
            }
        }

        public void UpdateCard(CardModel card)
        {
            var item = FindCard(card);

            if (item == null)
            {
                Debug.LogError("Unable to find Card");
                return;
            }

            item.Setup(card);
        }

        public void DestroyCard(CardModel card)
        {
            var item = FindCard(card);

            if (item == null)
            {
                Debug.LogError("Unable to find Card");
                return;
            }
            
            instancedCards.Remove(item);
            Destroy(item.gameObject);
        }

        public void DropCardOnPlayArea(UICard card)
        {
            cardsOnPlayArea.Clear();
            cardsOnPlayArea.Add(card);
        }

        private UICard FindCard(CardModel card)
        {
            var itemIndex = instancedCards.FindIndex(x => x.Index == card.index);
            if (itemIndex == -1)
            {
                Debug.LogError("Unable to find Card");
                return null;
            }

            return instancedCards[itemIndex];
        }
    }
}
