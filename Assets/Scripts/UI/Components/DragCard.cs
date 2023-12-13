using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardGame.Core.UI
{
    public class DragCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] public RectTransform rectTransform;
        [SerializeField] private Vector2 startPos;
        [SerializeField] private GameObject glowWhite;
        [SerializeField] private CanvasGroup cg;
        [SerializeField] private Image image;
        [SerializeField] internal GameObject glowGreen;

        private void Start()
        {
            image = GetComponent<Image>();
            rectTransform = GetComponent<RectTransform>();
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            startPos = rectTransform.anchoredPosition;
            cg.blocksRaycasts = false;
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            glowGreen.SetActive(true);
            var newPosition = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position);
            image.rectTransform.position = newPosition;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            cg.blocksRaycasts = true;
            glowGreen.SetActive(false);
            rectTransform.DOAnchorPos(startPos, 0.5f);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData data)
        {
            glowWhite.SetActive(true);
        }
        
        void IPointerExitHandler.OnPointerExit(PointerEventData data)
        {
            glowWhite.SetActive(false);
        }
    }
}
