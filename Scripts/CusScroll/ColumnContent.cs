using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ColumnContent : MonoBehaviour
{
    private List<string> colLoad;
    private Vector2 oldColBoundary;
    private Vector2 newColBoundary;


    private void Awake()
    {
        oldColBoundary = new Vector2(0f, 0f); 
    }

    

    public List<float> colObjWidths()
    {
        List<float> colWidths = new List<float>();

        for(int a = 0; a < this.transform.childCount; a++)
        {
            if (this.transform.GetChild(a).gameObject.activeSelf) colWidths.Add(this.transform.GetChild(a).GetComponent<RectTransform>().rect.width);
        }


        return colWidths;

        
    }

    public float ReturnHeight()
    {
        return this.transform.parent.parent.GetComponent<RectTransform>().rect.height;
    }

    public void CheckColBoundary(float rowOffset, float optionOffset)
    {
        this.newColBoundary[0] = rowOffset;
        this.newColBoundary[1] = optionOffset;

        if (!(newColBoundary == oldColBoundary))
        {
            oldColBoundary = newColBoundary;
            //change grid offsets
            this.transform.parent.parent.GetComponent<RectTransform>().offsetMin = new Vector2(rowOffset, 0f);
            this.transform.parent.parent.GetComponent<RectTransform>().offsetMax = new Vector2(-optionOffset, 0f);
            Debug.Log($"{this.transform.parent.name} is getting adjust by {rowOffset} and {optionOffset}, respectively");

        }
        CapColObj();
    }
    public void CallColumn(List<string> action, float offsetFromRowWidth)
    {
        this.transform.parent.GetComponent<RectTransform>().offsetMin = new Vector2(offsetFromRowWidth, 0f);
        this.colLoad = action;

        for (int a = 0; a < colLoad.Count; a++)
        {
            this.transform.GetChild(a).gameObject.SetActive(true);
            this.transform.GetChild(a).gameObject.GetComponent<TextMeshProUGUI>().text = colLoad[a];
        }
    }

    private Vector2 cappedObj(int childObj)
    {
        Vector2 vector2 = new Vector2(250f, this.transform.GetChild(childObj).GetComponent<RectTransform>().rect.height);
        return vector2;
    }

    private void CapColObj()
    {
        for(int a = 0; a < this.transform.childCount; a++)
        {

            if (this.transform.GetChild(a).GetComponent<RectTransform>().rect.width > 250f)
            {
                this.transform.GetChild(a).GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

                this.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta = cappedObj(a);

            }
        }
    }
    
}
