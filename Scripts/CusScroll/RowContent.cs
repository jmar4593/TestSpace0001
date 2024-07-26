using ES3Types;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Scroll;

public class RowContent : Scroll01
{
    private List<string> rowLoad;
    private float largestWidth;
    // Start is called before the first frame update
    private float oldSize;

    [SerializeField]
    private bool checkDone;

    public bool Prefd()
    {
        return ObjsPrefd(this.gameObject);
        
    }

    /// <summary>
    /// apart of rowY to gridY
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

    public void SetToPrefer()
    {
        PreferDims(this.gameObject);
    }

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
}
