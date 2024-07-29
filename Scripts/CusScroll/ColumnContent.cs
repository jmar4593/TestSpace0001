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

    /// <summary>
    /// This method is figuring out how tall the spaces inside ColumnContent, should be. Determined tallest height is then transposed
    /// into one of the three standard sizes, based on 1/7th of the scroll object height. Plugs directly into ExecutePreferred of CustomScroll.
    /// </summary>
    /// <param name="customScrollObject"></param>
    /// <returns></returns>
    public float AdjustY(GameObject customScrollObject)
    {
        float tallestHeight = 0; 
        for(int a = 0; a < this.transform.childCount; a++)
        {
            if(tallestHeight < this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.y)
            { 
                if (this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.y < 60)
                {
                    tallestHeight = customScrollObject.GetComponent<RectTransform>().rect.height * 0.071f;
                }
                if ((this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.y > 60) && (this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.y < 100) )
                {
                    tallestHeight = customScrollObject.GetComponent<RectTransform>().rect.height * 0.107f;
                }
                else
                {
                    tallestHeight = customScrollObject.GetComponent<RectTransform>().rect.height * 0.142f;
                }
            }
        }
        for(int b = 0; b < this.transform.childCount; b++)
        {
            this.transform.GetChild(b).GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            this.transform.GetChild(b).GetComponent<RectTransform>().sizeDelta = new Vector2(this.transform.GetChild(b).GetComponent<RectTransform>().sizeDelta.x, tallestHeight);
        }
        this.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(this.transform.parent.GetComponent<RectTransform>().sizeDelta.x, tallestHeight);
        return tallestHeight;
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
