using UnityEngine;
using UnityEngine.EventSystems;

public class DropTarget : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        Sorter sorter = dropped.GetComponent<Sorter>();
        if (sorter != null)
        {
            sorter.LandedOnTarget(gameObject.tag);
        }
    }

    // Optional: track when dragged item hovers over this target
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        Sorter sorter = eventData.pointerDrag.GetComponent<Sorter>();
        if (sorter != null)
            Debug.Log("Hovering over: " + gameObject.tag);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        Sorter sorter = eventData.pointerDrag.GetComponent<Sorter>();
        if (sorter != null)
            sorter.LeftTarget();
    }
}