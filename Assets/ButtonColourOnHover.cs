using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Text))]
public class ButtonColourOnHover : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler{

    Text theText;

    public Color hoverColor;
    Color nonHoverColor;

    private void Start()
    {
        theText = GetComponent<Text>();
        nonHoverColor = theText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = nonHoverColor;
    }
}
