using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class CustomScroll : Scroll02
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private ColumnContent colContent;

    [SerializeField]
    private RowContent roContent;

    [SerializeField]
    private GridContent griContent;

    [SerializeField]
    private OptionsContent optContent;

    [SerializeField]
    [Tooltip("Turn on to setup options in right corner of listed objects")]
    private bool optionsOn;


    private void Update()
    {
        ExecutePreferred(AllObjsPrefd(), this.gameObject.GetComponent<RectTransform>().rect.height / 10);

        if (Input.GetKeyDown(KeyCode.V))
        {
            SetToPrefer();

            Debug.Log($"Give me 16th({this.GetComponent<RectTransform>().rect.height*0.0625f}) 8th ({this.GetComponent<RectTransform>().rect.height * 0.125f}) and 3-16th ({this.GetComponent<RectTransform>().rect.height * 0.1875f}) total of this object");
        }

    }

    private void ExecutePreferred(bool allObjsPrefd, float tenPercentHeight)
    {
        if(allObjsPrefd)
        {
            //prepare widths
            float roOffsetX = roContent.AdjustX();
            float colOffsetX = tenPercentHeight;
            optContent.TurnOptions(optionsOn, colOffsetX);
            colContent.AdjustX(OptimizedFloats(colContent.ReturnColumnX(), griContent.CapGridX), roOffsetX, colOffsetX);
            griContent.AdjustX(OptimizedFloats(colContent.ReturnColumnX(), griContent.CapGridX), roOffsetX, colOffsetX);

            //prepare heights
            float colOffsetY = colContent.AdjustY(this.gameObject);
            roContent.AdjustY(OptimizedFloats(roContent.ReturnRowY(), griContent.CapGridY), colOffsetY);
            griContent.AdjustY(OptimizedFloats(roContent.ReturnRowY(), griContent.CapGridY),colOffsetY);
        }
    }

    /// <summary>
    /// Trigger this to set in motion correct autosizing of scroll Object.
    /// </summary>
    private void SetToPrefer()
    {
        roContent.SetToPrefer();
        colContent.SetToPrefer();
        griContent.SetToPrefer();
    }

    private bool AllObjsPrefd()
    {
        bool allObjPrefd = false;
        if(roContent.Prefd() && colContent.Prefd() && griContent.Prefd()) allObjPrefd = true;
        return allObjPrefd;
    }
    
}
