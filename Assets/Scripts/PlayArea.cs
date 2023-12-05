using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayArea : MonoBehaviour, IDropHandler
{
    public DeckController deckController;
    public RectTransform cardSlot;

    public void OnDrop(PointerEventData eventData)
    {
        var playArea = eventData.pointerDrag.GetComponent<DragCard>();

        if (!playArea) return;
        
        deckController.DropCardOnPlayArea(eventData.pointerDrag.GetComponent<UICard>());
        
        var rectTransform = playArea.rectTransform;
        var worldPosition = rectTransform.position;

        playArea.glowGreen.SetActive(false);
        
        Destroy(playArea.GetComponent<DragCard>());

        rectTransform.SetParent(transform, true);
        rectTransform.anchoredPosition = cardSlot.anchoredPosition;
        rectTransform.position = worldPosition;
        rectTransform.DOAnchorPos(Vector2.zero, 0.5f);
        rectTransform.DORotate(Vector2.zero, 0.5f);
    }
}