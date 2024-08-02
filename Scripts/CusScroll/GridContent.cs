using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Scroll;
using UnityEngine.UI;

public class GridContent : Scroll01
{
    /// <summary>
    /// Returns true if RowContent children have resolved to their preferred sizes, via ContentSizeFitter.
    /// </summary>
    /// <returns></returns>
    public bool Prefd()
    {
        bool prefd = true;
        for (int a = 0; a < this.transform.childCount; a++)
        {
            prefd = ObjsPrefd(this.transform.GetChild(a).gameObject);
        }
        return prefd;
    }

    /// <summary>
    /// Set Grid object children dims to preferred, via ContentSizeFitter
    /// </summary>
    public void SetToPrefer()
    {
        for (int a = 0; a < this.transform.childCount; a++)
        {
            PreferDims(this.transform.GetChild(a).gameObject);
        }

    }

    /// <summary>
    /// Returns the longest X dim of the linearly adjacent col, by the signature int colX. Nested inside OptmizedFloats method
    /// called from CustomScroll.
    /// </summary>
    /// <param name="colX"></param>
    /// <returns></returns>
    public float WidestGridX(int colX)
    {
        float longestWidthGridX = new float();
        for (int a = 0; a < this.transform.childCount; a++)
        {
            if (longestWidthGridX < this.transform.GetChild(a).GetChild(colX).GetComponent<RectTransform>().sizeDelta.x)
                longestWidthGridX = this.transform.GetChild(a).GetChild(colX).GetComponent<RectTransform>().sizeDelta.x;
        }
        return longestWidthGridX;
    }

    /// <summary>
    /// Returns the tallest Y dim of the linearly adjacent row, by the signature int rowY. Nested inside OptmizedFloats method
    /// called from CustomScroll.
    /// </summary>
    /// <param name="rowY"></param>
    /// <returns></returns>
    public int ReturnOptLineCount(int rowY)
    {
        int optLineCount = 0;
        for (int a = 0; a < this.transform.childCount; a++)
        {
            if (optLineCount < this.transform.GetChild(rowY).GetChild(a).GetComponent<TextMeshProUGUI>().textInfo.lineCount)
                optLineCount = this.transform.GetChild(rowY).GetChild(a).GetComponent<TextMeshProUGUI>().textInfo.lineCount;
        }
        return optLineCount;
    }

    /// <summary>
    /// Adjusts all X dims of the Grid by a returned value of the best fit X dim (optmzedWidths). Called in the ExecutePreferred method from
    /// CustomScroll
    /// </summary>
    /// <param name="optmzedWidths"></param>
    /// <param name="rowOffset"></param>
    /// <param name="optionsOffset"></param>
    public void AdjustX(List<float> optmzedWidths, float rowOffset, float optionsOffset)
    {
        for (int a = 0; a < this.transform.childCount; a++)
        {
            for (int b = 0; b < this.transform.GetChild(a).childCount; b++)
            {
                this.transform.GetChild(a).GetChild(b).GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                this.transform.GetChild(a).GetChild(b).GetComponent<RectTransform>().sizeDelta = new Vector2(optmzedWidths[b], this.transform.GetChild(a).GetChild(b).GetComponent<RectTransform>().sizeDelta.y);
            }
        }
        OffsetRowOptionBorders(this.gameObject, rowOffset, optionsOffset);
    }

    /// <summary>
    /// Adjusts all Y dims of the Grid by a returned value of the best fit Y dim (optmzedHeights). Called in the ExecutePreferred method from
    /// CustomScroll
    /// </summary>
    /// <param name="optmzedHeights"></param>
    /// <param name="colOffset"></param>
    public void AdjustY(List<float> optmzedHeights, float colOffset)
    {
        for (int a = 0; a < this.transform.childCount; a++)
        {
            for (int b = 0; b < this.transform.GetChild(a).childCount; b++)
            {
                this.transform.GetChild(a).GetChild(b).GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.Unconstrained;
                this.transform.GetChild(a).GetChild(b).GetComponent<RectTransform>().sizeDelta = new Vector2(this.transform.GetChild(a).GetChild(b).GetComponent<RectTransform>().sizeDelta.x, optmzedHeights[a]);
                this.transform.GetChild(a).GetChild(b).GetComponent<TextMeshProUGUI>().overflowMode = TextOverflowModes.Ellipsis;
            }
        }
        OffsetColBorder(this.gameObject, colOffset);
    }
}
