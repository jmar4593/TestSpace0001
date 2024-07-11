using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class GridContent : MonoBehaviour
{
    private List<List<string>> gridLoad;

    private Vector2 oldGridBoundary;
    private Vector2 newGridBoundary;

    

    /// <summary>
    /// this method should originally sit on the grid object
    /// </summary>
    /// <param name="rowBoundary"></param>
    /// <param name="optionOffset"></param>
    /// 
    public void Awake()
    {
        oldGridBoundary = new Vector2(0f, 0f);
    }

    //need to start with row first because we need to see how many objects are active.
    public void GridSetGroupingObjs(List<float> rowObjHeights, List<float> colObjWidths)
    {
        //set groupings first
        for (int b = 0; b < rowObjHeights.Count; b++)
        {
            this.transform.GetChild(b).gameObject.SetActive(true);
        }
        //then set grouping objs. active and sizes
        for (int a = 0; a < this.transform.childCount; a++)
        {
            if(this.transform.GetChild(a).gameObject.activeSelf)
            {
                for(int b = 0; b < colObjWidths.Count; b++)
                {
                    this.transform.GetChild(a).GetChild(b).gameObject.SetActive(true);
                    this.transform.GetChild(a).GetChild(b).GetComponent<RectTransform>().sizeDelta = new Vector2(colObjWidths[b], rowObjHeights[b]);
                }
            }
        }
    }



    public void CheckGridBoudaries(float rowOffset, float optionOffset)
    {
        this.newGridBoundary[0] = rowOffset;
        this.newGridBoundary[1] = optionOffset;

        if (!(newGridBoundary == oldGridBoundary))
        {
            oldGridBoundary = newGridBoundary;
            //change grid offsets
            this.transform.parent.parent.GetComponent<RectTransform>().offsetMin = new Vector2(rowOffset, 0f);
            this.transform.parent.parent.GetComponent<RectTransform>().offsetMax = new Vector2(-optionOffset, 0f);

        }
    }



    public void CallGrid(List<List<string>> insertGridLoad, float offsetFromRowWidth)
    {
        this.transform.parent.GetComponent<RectTransform>().offsetMin = new Vector2(offsetFromRowWidth, 0f);

        this.gridLoad = insertGridLoad;


        for (int a = 0; a < insertGridLoad.Count; a++)
        {
            for (int b = 0; b < insertGridLoad[a].Count; b++)
            {
                this.transform.GetChild(a).GetChild(b).gameObject.SetActive(true);
                this.transform.GetChild(a).GetChild(b).GetComponent<TextMeshProUGUI>().text = insertGridLoad[a][b];
            }
        }
    }
    public void CallGrid(List<List<string>> insertGridLoad, float offsetFromRowWidth, float offsetFromOptionsWidth)
    {

        this.transform.parent.parent.GetComponent<RectTransform>().offsetMin = new Vector2(offsetFromRowWidth, 0f);
        this.transform.parent.parent.GetComponent<RectTransform>().offsetMax = new Vector2(-offsetFromOptionsWidth, 0f);
        
        this.gridLoad = insertGridLoad;


        for(int a = 0; a < insertGridLoad.Count; a++)
        {
            for(int b = 0; b < insertGridLoad[a].Count; b++)
            {
                this.transform.GetChild(a).GetChild(b).gameObject.SetActive(true);
                this.transform.GetChild(a).GetChild(b).GetComponent<TextMeshProUGUI>().text = insertGridLoad[a][b];
            }
        }
    }
}
