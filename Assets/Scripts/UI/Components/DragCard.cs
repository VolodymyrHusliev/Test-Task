using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public RectTransform rectTransform;
    [SerializeField] public Vector2 startPos;
    [SerializeField] private GameObject glowWhite;
    [SerializeField] private CanvasGroup cg;
    [SerializeField] public Image image;
     public GameObject glowGreen;

    private void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = rectTransform.anchoredPosition;
        cg.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        glowGreen.SetActive(true);
        var newPosition = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position);
        image.rectTransform.position = newPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        cg.blocksRaycasts = true;
        glowGreen.SetActive(false);
        rectTransform.DOAnchorPos(startPos, 0.5f);
    }

    public void OnPointerEnter(PointerEventData data)
    {
        glowWhite.SetActive(true);
    }
    
    public void OnPointerExit(PointerEventData data)
    {
        glowWhite.SetActive(false);
        
    }
}
