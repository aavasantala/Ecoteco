using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Scroller : MonoBehaviour
{
    public Scrollbar target;
    public Button theOtherButton;
    public float Step = 0.1f;

    public void Increment()
    {
        if (target == null || theOtherButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
        target.value = Mathf.Clamp(target.value + Step, 0, 1);
        GetComponent<Button>().interactable = target.value != 1;
        theOtherButton.interactable = true;
    }

    public void Decrement()
    {
        if (target == null || theOtherButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
        target.value = Mathf.Clamp(target.value - Step, 0, 1);
        GetComponent<Button>().interactable = target.value != 0; ;
        theOtherButton.interactable = true;
    }
}
