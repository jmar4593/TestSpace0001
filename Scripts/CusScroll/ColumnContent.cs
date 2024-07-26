using Scroll;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ColumnContent : Scroll01
{
    public bool Prefd()
    {        return ObjsPrefd(this.gameObject);
    }

    public List<float> ReturnColumnX()
    {
        List<float> colX = new List<float>();

        for(int a = 0; a < this.transform.childCount; a++)
        {
            colX.Add(this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.x);
        }

        return colX;
    }
    
    public void AdjustX(List<float> optmzdWidths, float rowOffset, float optionsOffset)
    {
        for(int a = 0; a < this.transform.childCount; a++)
        {
            this.transform.GetChild(a).GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta = new Vector2(optmzdWidths[a], this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.y);
        }
        OffsetRowOptionBorders(this.gameObject, rowOffset, optionsOffset);

    }

    public void AdjustY(GameObject customScrollObject)
    {
        float tallestHeight = 0;
        for(int a = 0; a < this.transform.childCount; a++)
        {
            if(tallestHeight < this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.y)
            {
                if (this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.y > 110)
                {
                    tallestHeight = customScrollObject.GetComponent<RectTransform>().rect.height * 0.3f;
                }
                if ((this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.y > 70) && (this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.y < 110) )
                {
                    tallestHeight = 150;
                }
            }
        }
    }

    public void SetToPrefer()
    {
        PreferDims(this.gameObject);
    }

    public float ReturnContentHeight()
    {
        return this.GetComponent<RectTransform>().sizeDelta.y;
    }
}
