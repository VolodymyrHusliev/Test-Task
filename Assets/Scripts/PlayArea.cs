using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardGame.Core.UI
{
    public class PlayArea : MonoBehaviour, IDropHandler
    {
        [SerializeField] private DeckController deckController;
        [SerializeField] private RectTransform cardSlot;

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            var playArea = eventData.pointerDrag.GetComponent<DragCard>();
            
            if (!playArea) return;

            var item = eventData.pointerDrag.GetComponent<UICard>();
            
            if (item == null)
            {
                Debug.LogError("Unable to find card");
                return;
            }
            
            deckController.DropCardOnPlayArea(item);

            var rectTransform = playArea.rectTransform;
            var worldPosition = rectTransform.position;

            playArea.glowGreen.SetActive(false);
            
            Destroy(playArea);

            rectTransform.SetParent(transform, true);
            rectTransform.anchoredPosition = cardSlot.anchoredPosition;
            rectTransform.position = worldPosition;
            rectTransform.DOAnchorPos(Vector2.zero, 0.5f);
            rectTransform.DORotate(Vector2.zero, 0.5f);
        }
    }
}

