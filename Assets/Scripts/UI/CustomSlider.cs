using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSlider : MonoBehaviour {

    public RectTransform fill;

    [Range(0,1)]
    public float value = 0.5f;
    public float maxValue = 1;
    public float minValue = 0;


    private void OnGUI()
    {
        float maxWidth = GetComponent<RectTransform>().rect.width;
        //clamp the value
        value = Mathf.Clamp(value, minValue, maxValue);


        fill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,  (maxWidth * value));
    }
}
