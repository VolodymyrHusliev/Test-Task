using System;
using System.Collections;
using System.Collections.Generic;
using CardGame.Core.UI;
using CardGame.Core.Utils;
using CardGame.Networking.Models;
using UnityEngine;
using UnityEngine.Networking;

using Random = UnityEngine.Random;

namespace CardGame.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private DeckController deckController;
        [SerializeField] private RandomCardValue randomCardValueUI;
        
        private List<CardModel> cardsData = new();
        
        private void Awake()
        {
#if UNITY_EDITOR
            randomCardValueUI.Setup(RandomCardValue);
#endif    
        }

        private IEnumerator Start()
        {
            var cardsCount = Random.Range(4, 7);
            var index = 0;
            
            while(index < cardsCount)
            {
                var item = new CardModel();
                item.index = index;
                item.attack = Random.Range(2, 10);
                item.hp = Random.Range(2, 10);
                item.mana = Random.Range(2, 10);
                
                yield return GetRandomImage((image) =>
                {
                    item.icon = image;
                });

                cardsData.Add(item);
                index++;
            }
            
            deckController.SetupDeck(cardsData);
            yield return null;
        }

        private IEnumerator GetRandomImage(Action<Texture2D> onDone)
        {
            var RANDOM_IMAGE_URL = "https://picsum.photos/200/300";
            var downloadHandlerTexture = new DownloadHandlerTexture();
            
            yield return GetRequestEnumerator(RANDOM_IMAGE_URL, downloadHandlerTexture);

            onDone.Invoke(downloadHandlerTexture.texture);
        }
        
        private IEnumerator GetRequestEnumerator(string url, DownloadHandler downloadHandler)
        {
            var webRequest = new UnityWebRequest(url);
            webRequest.downloadHandler = downloadHandler;

            yield return webRequest.SendWebRequest();
        }

#if UNITY_EDITOR
        
        private int cardIndex = 0;
        
        private void RandomCardValue()
        {
            var cards = cardsData;
            
            if(cardsData.Count == 0)
                return;

            if (cardIndex >= cards.Count)
            {
                cardIndex = 0;
            }
            
            var rValue = Random.Range(0, 3);
            var card = cards[cardIndex];
            
            switch (rValue)
            {
                case 0:
                    card.hp = Random.Range(0, 10);
                    break;
                case 1:
                    card.mana = Random.Range(0, 10);
                    break;
                case 2:
                    card.attack = Random.Range(0, 10);
                    break;
            }

            if (card.hp > 0)
            {
                deckController.UpdateCard(card);
            }
            else
            {
                cardsData.RemoveAt(cardIndex);
                deckController.DestroyCard(card);
            }
            
            cardIndex++;
            
            if (cardIndex >= cardsData.Count)
            {
                cardIndex = 0;
            }
        }
        
#endif
        
    }
}

