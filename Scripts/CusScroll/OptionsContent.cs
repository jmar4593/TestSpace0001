using Scroll;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsContent : Scroll01
{
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

    /// <summary>
    /// Adjusts all optObjs to a standard width (about equal to a one line object heigt of column [1/14th the total height of scroll obj])
    /// Called from CustomScroll.
    /// </summary>
    /// <param name="offsetRowBorder"></param>
    public void AdjustX(float offsetRowBorder)
    {
        for(int a = 0; a < this.transform.childCount; a++)
        {
            this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta = new Vector2(offsetRowBorder, this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.y);
        }
    }

    /// <summary>
    /// Adjusts all optObjs to their optimized heights, which are taken from OptmzedFloats method.
    /// Called from CustomScroll.
    /// </summary>
    /// <param name="optmzdHeights"></param>
    /// <param name="offsetColY"></param>
    public void AdjustY(List<float> optmzdHeights, float offsetColY)
    {
        for(int a = 0; a < this.transform.childCount; a++)
        {
            this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta = new Vector2(this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.x, optmzdHeights[a]);
        }
        OffsetColBorder(this.gameObject, offsetColY);
    }
}
