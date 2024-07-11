using ES3Types;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RowContent : MonoBehaviour
{
    private List<string> rowLoad;
    private float largestWidth;
    // Start is called before the first frame update
    private Vector2 oldSize;
    private Vector2 newSize;


    [SerializeField]
    private bool checkDone;
    private void ConSizTest()
    {
        //set method's oldSize value on start.
        ///so this was a smaller
        ///method to begin with so oldSize, would be the agreed upon balanced optimal size inside all obj groupings.
        ///There are four of them
        ///row widths to themselves;
        ///column heights to themselves;
        ///row obj height to its matching grid row objects;
        ///column obj height to its matching grid column objects

        //set this method on update to start, and make sure that checkdone is set to false to start method when ready.
        newSize = this.GetComponent<RectTransform>().sizeDelta;
        if (!checkDone)
        {

            if ((newSize == oldSize))
            {
                //enter method for changing size to preferred, via content fitter
            }

            if (!(newSize == oldSize))
            {
                //enter method to record all preferred sizes from content size fitter

                //enter method to disable content fitter, AND size all obj to balanced optimal size (handled by record of content size fitter)

                //set checkdone to true to stop method from running completely
                checkDone = true;
            }
        }


    }
    public void ShoutDims()
    {
        for(int g = 0; g < this.transform.childCount; g++)
        {
            Debug.Log(this.transform.GetChild(g).GetComponent<RectTransform>().sizeDelta);
        }
    }

    public void UnconstrainFitObjects()
    {
        for(int e = 0; e < this.transform.childCount; e++)
        {
            this.transform.GetChild(e).GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            this.transform.GetChild(e).GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        }
    }

    public void PreferFitObjects()
    {
        for(int f = 0; f <  this.transform.childCount; f++)
        {
            this.transform.GetChild(f).GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            this.transform.GetChild(f).GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
    }

    public List<float> RowObjHeights()
    {
        List<float> rowHeights = new List<float>();
        for (int d = 0; d < this.transform.childCount; d++)
        {
            if (this.transform.GetChild(d).gameObject.activeSelf) rowHeights.Add(this.transform.GetChild(d).GetComponent<RectTransform>().rect.height);
        }
        return rowHeights;
    }
    public float ReturnWidth()
    {
        return this.largestWidth;
    }
    public void SetWidth()
    {
        largestWidth = 0f;
        for (int a = 0; a < this.transform.childCount; a++)
        {
            if (this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.x > largestWidth)
            {
                largestWidth = this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta.x;
            }
        }
        //ok great these sized the parents nicely, but the text objects themselves are still not correct. set it too early here, need to set it later.

        if (largestWidth < 100f)
        {
            largestWidth = 100f;

        }
        if (largestWidth > 250f)
        {
            largestWidth = 250f;

        }

        this.transform.parent.GetComponentInParent<RectTransform>().sizeDelta = new Vector2(largestWidth, this.transform.parent.GetComponentInParent<RectTransform>().sizeDelta.y);

        //size down the individual row objects that are bigger than my largest cap.
        for(int b = 0; b < this.transform.childCount; b++)
        {
            if(this.transform.GetChild(b).GetComponent<RectTransform>().sizeDelta.x > largestWidth)
            {
                this.transform.GetChild(b).GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                this.transform.GetChild(b).GetComponent<RectTransform>().sizeDelta = new Vector2(largestWidth, this.transform.GetChild(b).GetComponent<RectTransform>().sizeDelta.y);
            }
        }
    }


    public void CallRow(List<string> calledRowLoad)
    {
        this.rowLoad = calledRowLoad;

        for (int a = 0; a < rowLoad.Count; a++)
        {
            this.transform.GetChild(a).gameObject.SetActive(true);
            this.transform.GetChild(a).gameObject.GetComponent<TextMeshProUGUI>().text = rowLoad[a];
        }

    }



}
