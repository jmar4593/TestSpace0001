using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsContent : MonoBehaviour
{
    private float largestWidth;
    private bool oldOnCond;
    private bool newOnCond;

    /// <summary>
    /// This method will be directly attached to the options button, once I figure out what thats gonna look like.
    /// </summary>
    /// <param name="optionsOn"></param>
    /// <param name="colHeight"></param>
    public void TurnOptions(bool optionsOn, float colHeight)
    {
        if (optionsOn)
        {
            this.GetComponentInParent<RectTransform>().offsetMin = new Vector2(-colHeight, 0f);

        }

    }

    public float ReturnWidth()
    {
        return this.GetComponent<RectTransform>().rect.width;
    }


}
