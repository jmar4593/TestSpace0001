using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CustSizeFitter : MonoBehaviour
{
    public GameObject canvas;
    public GameObject columnViewport;
    public GameObject rowViewport;

    private List<string> rowList;
    private List<string> colList;

    [SerializeField]
    private PopulateColumnRow popColRow;
    // Start is called before the first frame update
    void Start()
    {
        NewRowColList();
        popColRow.DefineRowAndColumn(rowList, colList);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(GetLargestChildWidth(), this.GetComponent<RectTransform>().sizeDelta.y);
        columnViewport.GetComponent<RectTransform>().offsetMin = new Vector2(GetLargestChildWidth(), 0f);
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    private float GetLargestChildWidth()
    {
        float checkWidth = 0;
        float largestWidth = 0;
        for(int a = 0; a < this.transform.GetChild(0).childCount; a++)
        {
            checkWidth = this.transform.GetChild(0).GetChild(a).GetComponent<RectTransform>().sizeDelta.x;
            if (checkWidth > largestWidth) largestWidth = checkWidth;
        }
        float trueLargestWidth = largestWidth * Screen.height / canvas.GetComponent<RectTransform>().rect.height;
        return largestWidth;
    }

    private void NewRowColList()
    {
        rowList = new List<string> { "string","theory","ball"};
        colList = new List<string> { "raider", "pop", "bubblegum" };
    }
}
