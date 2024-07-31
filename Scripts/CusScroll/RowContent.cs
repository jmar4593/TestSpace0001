using ES3Types;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Scroll;

public class RowContent : Scroll01
{    
    /// <summary>
    /// Returns true if RowContent children have resolved to their preferred sizes, via ContentSizeFitter.
    /// </summary>
    /// <returns></returns>
    public bool Prefd()
    {
        return ObjsPrefd(this.gameObject);
    }

    /// <summary>
    /// Returns a list of Y dims from the Row object, to be compared against linearly adjacent Y dim values within the Grid object.
    /// Called from OptimizedFloats method, in the CustomScroll file.
    /// </summary>
    /// <returns></returns>
    public List<float> ReturnRowY()
    {
        List<float> rowY = new List<float>();
        for(int a = 0; a < this.transform.childCount; a++)
        {
            rowY.Add(this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.y);
        }
        return rowY;
    }

    /// <summary>
    /// Set Row object children dims to preferred, via ContentSizeFitter
    /// </summary>
    public void SetToPrefer()
    {
        PreferDims(this.gameObject);
    }

    /// <summary>
    /// Returns the longest child width from the Row Object, to be used in determining the X border for Row. Also, optimizes Row objects to the best fit width.
    /// </summary>
    /// <returns></returns>
    public float AdjustX()
    {
        float longestWidth = 0;

        for (int a = 0; a < this.transform.childCount; a++)
        {
            if (longestWidth < this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.x)
            {
                if(this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.x > 250)
                {
                    longestWidth = 250;
                }
                else longestWidth = this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.x;
            }
        }
        for(int b = 0; b < this.transform.childCount; b++)
        {
            this.transform.GetChild(b).GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            this.transform.GetChild(b).GetComponent<RectTransform>().sizeDelta = new Vector2(longestWidth, this.transform.GetChild(b).GetComponent<RectTransform>().sizeDelta.y);
        }
        this.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(longestWidth, this.transform.parent.GetComponent<RectTransform>().sizeDelta.y);
        return longestWidth;
    }

    /// <summary>
    /// Optimizes Row objects to the best fit heights, based on a List of floats returned from OptimizedFloats method called in CustomScroll file.
    /// </summary>
    /// <param name="optmzdHeights"></param>
    /// <param name="colOffsetY"></param>
    public void AdjustY(List<float> optmzdHeights, float colOffsetY)
    {
        for(int a = 0; a < this.transform.childCount; a++)
        {
            this.transform.GetChild(a).GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta = new Vector2(this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.x, optmzdHeights[a]);
            this.transform.GetChild(a).GetComponent<TextMeshProUGUI>().overflowMode = TextOverflowModes.Ellipsis;
        }
        OffsetColBorder(this.gameObject, colOffsetY);
    }
}
