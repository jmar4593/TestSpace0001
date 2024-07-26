using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Scroll;
using UnityEngine.UI;

public class GridContent : Scroll01
{
    public bool Prefd()
    {
        bool prefd = true;
        for(int a = 0; a < this.transform.childCount; a++)
        {
            prefd = ObjsPrefd(this.transform.GetChild(a).gameObject);
        }

        return prefd;
    }

    public void SetToPrefer()
    {
        for(int a = 0; a < this.transform.childCount; a++)
        {
            PreferDims(this.transform.GetChild(a).gameObject);
        }

    }

    public float CapGridX(int colX)
    {
        float longestWidthGridX = new float();
        for(int a = 0; a < this.transform.childCount; a++)
        {
            if (longestWidthGridX < this.transform.GetChild(a).GetChild(colX).GetComponent<RectTransform>().sizeDelta.x)
                longestWidthGridX = this.transform.GetChild(a).GetChild(colX).GetComponent<RectTransform>().sizeDelta.x;
        }
        return longestWidthGridX;
    }

    public void AdjustX(List<float> optmzedWidths, float rowOffset, float optionsOffset)
    {
        for(int a = 0; a < this.transform.childCount; a++)
        {
            for(int b = 0; b < this.transform.GetChild(a).childCount; b++)
            {
                this.transform.GetChild(a).GetChild(b).GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                this.transform.GetChild(a).GetChild(b).GetComponent<RectTransform>().sizeDelta = new Vector2(optmzedWidths[b], this.transform.GetChild(a).GetChild(b).GetComponent<RectTransform>().sizeDelta.y);
            }
        }

        OffsetRowOptionBorders(this.gameObject, rowOffset, optionsOffset);

    }
}
