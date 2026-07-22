using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragger : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    public RectTransform canvasRect;
    private Canvas canvas;
    private Vector2 ogPos2;
    public bool onBorder;
    private Rigidbody2D rb;

    public float linearDrag = 10f;
    public float velocityMultiplier = 1f;
    public float bounceFactor = 0.6f;

    public string recyclableTag;
    private bool isRecycled = false;

    private Vector2 currentVelocity;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0f;
        rb.linearDamping = linearDrag;
        StopPhysics();

        RandomizeOgPos();
    }

    private void Update()
    {
        if(!(rb.angularVelocity> 0f))
        {
            return;
        }
        rb.angularVelocity = rb.angularVelocity - 1;
    }

    void RandomizeOgPos()
    {
        ogPos2.x = Random.Range(-242, 242);
        ogPos2.y = Random.Range(-260, -85);
    }

    void StopPhysics()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta / canvas.scaleFactor;
        rectTransform.anchoredPosition += delta;

        currentVelocity = delta / Time.unscaledDeltaTime;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.DOKill();
        canvasGroup.blocksRaycasts = false;
        StopPhysics();
        currentVelocity = Vector2.zero;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        RandomizeOgPos();

        if (onBorder)
        {
            StopPhysics();

            rectTransform.DOKill();
            rectTransform
                .DOAnchorPos(ogPos2, 0.4f)
                .SetEase(Ease.OutCirc)
                .SetUpdate(true);
        }
        else if (currentVelocity.sqrMagnitude > 0.01f)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.linearVelocity = currentVelocity * velocityMultiplier;
            
        }
        else
        {
            StopPhysics();
        }

        canvasGroup.blocksRaycasts = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("border"))
        {
            onBorder = true;

            if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                Vector2 closestPoint = collision.ClosestPoint(rectTransform.position);
                Vector2 normal = ((Vector2)rectTransform.position - closestPoint).normalized;

                if (normal.sqrMagnitude < 0.001f)
                    normal = -rb.linearVelocity.normalized;

                rb.linearVelocity = Vector2.Reflect(rb.linearVelocity, normal) * bounceFactor;
            }
        }
        else if (collision.tag.EndsWith("box"))
        {
            onBorder = false;

            if (isRecycled) return;

            string expectedBoxTag = gameObject.tag + "box"; // e.g. "seka" + "box" = "sekabox"

            if (collision.CompareTag(expectedBoxTag))
            {
                isRecycled = true;
                rectTransform.DOKill();
                StopPhysics();
                canvasGroup.blocksRaycasts = false;
                this.enabled = false;

                rectTransform
                    .DOScale(0f, 0.25f)
                    .SetEase(Ease.InBack)
                    .SetUpdate(true)
                    .OnComplete(() =>
                    {
                        GameManager.Instance.OnItemRecycled();
                        Destroy(gameObject);
                    });
            }
            // wrong box — do nothing special, item just stays draggable
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("border"))
            onBorder = false;
    }
}