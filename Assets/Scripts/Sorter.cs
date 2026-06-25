using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Sorter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    private Vector2 ogPos;
    private bool onBox = false;
    private BoxResult boxResult;
    private itemChooser itemChooser;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    public RectTransform canvasRect;
    private Canvas canvas;

    void Start()
    {
        boxResult=Singleton.Instance.boxResult;
        itemChooser = Singleton.Instance.itemChooser;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        Vector2 anchoredPos;
        ogPos.x = 28.926f;
        ogPos.y = -342.47f;
        
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect,ogPos,Camera.main,out anchoredPos);
        rectTransform.localPosition = ogPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.DOKill(); // stop any ongoing tween
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (!onBox)
            ReturnToOrigin();
    }

    private void ReturnToOrigin()
    {
        rectTransform.DOKill();
        rectTransform
            .DOAnchorPos(ogPos, 0.4f)
            .SetEase(Ease.OutBack)   
            .SetUpdate(true);
    }

    public void LandedOnTarget(string targetTag)
    {
        if (targetTag == "bg" && targetTag == "Respawn")
        {
            return;
        }
        onBox = true;
        Debug.Log("Landed on: " + targetTag);

        //väärä laatikko
        if (targetTag != gameObject.tag)
        {
            itemChooser.ChooseItem();
            
            Destroy(gameObject);
            boxResult.WrongBox();
            return;
        }

        //tähän oikea laatikko
        boxResult.CorrectBox();
        Destroy(gameObject);
        itemChooser.ChooseItem();
        
        return;
    }

    public void LeftTarget()
    {
        onBox = false;
    }
}