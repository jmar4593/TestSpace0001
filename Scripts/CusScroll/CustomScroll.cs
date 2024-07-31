using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class CustomScroll : Scroll02
{
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

    private bool autosizeScroll;


    private void Update()
    {
        //This method stays in preferred, but does not execute until I set a bool to do it.
        ExecutePreferred(AllObjsPrefd(), 50);
        if (Input.GetKeyDown(KeyCode.V))
        {
            SetToPrefer();
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
            List<float> bestColXs = OptimizedFloatsX(colContent.ReturnColumnX(), griContent.WidestGridX);
            colContent.AdjustX(bestColXs, roOffsetX, colOffsetX);
            griContent.AdjustX(bestColXs, roOffsetX, colOffsetX);
            optContent.AdjustX(50);

            //prepare heights
            float colOffsetY = colContent.AdjustY(this.gameObject);
            List<float> bestRowYs = OptimizedFloatsY(roContent.ReturnRowY(), griContent.TallestGridY);
            roContent.AdjustY(bestRowYs, colOffsetY);
            griContent.AdjustY(bestRowYs, colOffsetY);
            optContent.AdjustY(bestRowYs, colOffsetY);
            autosizeScroll = false;
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
        autosizeScroll = true;
    }

    private bool AllObjsPrefd()
    {
        bool allObjPrefd = false;
        if(autosizeScroll == true)
        {
            if (roContent.Prefd() && colContent.Prefd() && griContent.Prefd()) allObjPrefd = true;
        }
        return allObjPrefd;
    }
}
