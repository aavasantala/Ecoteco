using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Sorter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    private Vector2 ogPos;
    private Vector2 ogPos2;
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
        
        ogPos.x = 0f;
        ogPos.y = -380f;

        ogPos2.x = 0f;
        ogPos2.y = 262f;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect,ogPos,Camera.main,out anchoredPos);
        rectTransform.localPosition = ogPos;


    }

    public void OnRenderObject()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.DOKill();
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
            .DOAnchorPos(ogPos2, 0.4f)
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
            //itemChooser.ChooseItem();
            
            Destroy(gameObject);
            boxResult.WrongBox();
            return;
        }

        //tähän oikea laatikko
        boxResult.CorrectBox();
        Destroy(gameObject);
        //itemChooser.ChooseItem();
        
        return;
    }

    public void LeftTarget()
    {
        onBox = false;
    }
}