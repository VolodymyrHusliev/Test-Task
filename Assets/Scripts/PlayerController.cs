using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

namespace Card
{
    public class PlayerController : MonoBehaviour
    {
        private DeckController deckController;
        private int cardsCount;
        
        private IEnumerator Start()
        {
            cardsCount = Random.Range(4, 7);
            var cardsData = new List<CardModel>();
            var index = 0;
            
            while(index < cardsCount)
            {
                var item = new CardModel();
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
    }
}

