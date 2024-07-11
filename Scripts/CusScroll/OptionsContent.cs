using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsContent : MonoBehaviour
{
    private bool doneCheckingForWidestWidth;
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
            this.GetComponent<RectTransform>().offsetMin = new Vector2(-colHeight, 0f);
            this.GetComponentInParent<RectTransform>().offsetMin = new Vector2(-colHeight, 0f);

        }
    }

    public void TurnOffCheck()
    {
        doneCheckingForWidestWidth = true;
    }

    public float ReturnWidth()
    {
        return this.GetComponent<RectTransform>().rect.width;
    }
    public void SetWidth(float colHeight)
    {
        if (!doneCheckingForWidestWidth)
        {
            for (int a = 0; a < this.transform.childCount; a++)
            {
                if (this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.x > largestWidth)
                {
                    largestWidth = this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.x;
                }
            }
            this.transform.parent.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(colHeight, this.transform.parent.parent.GetComponent<RectTransform>().sizeDelta.y);
        }

    }

    public void CallOptions(bool optExists, int rowObjects)
    {
        for(int a = 0; a < rowObjects; a++)
        {
            this.transform.GetChild(a).gameObject.SetActive(true);
        }

    }

}
