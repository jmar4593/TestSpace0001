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
        ExecutePreferred(AllObjsPrefd());
        if (Input.GetKeyDown(KeyCode.V))
        {
            SetToPrefer();
        }
    }

    private void ExecutePreferred(bool allObjsPrefd)
    {
        if(allObjsPrefd)
        {
            float[] lineLevel = new float[3];
            lineLevel[0] = 0.071f * this.GetComponent<RectTransform>().rect.height;
            lineLevel[1] = 0.107f * this.GetComponent<RectTransform>().rect.height;
            lineLevel[2] = 0.142f * this.GetComponent<RectTransform>().rect.height;
            Debug.Log($"this y rect is {this.GetComponent<RectTransform>().rect.height}");
            Debug.Log($"line size 0: {lineLevel[0]}, line size 1: {lineLevel[1]}, line size 2: {lineLevel[2]}");

            //prepare widths
            float roOffsetX = roContent.AdjustX();
            optContent.TurnOptions(optionsOn, lineLevel[0]);
            List<float> bestColXs = OptimizedFloatsX(colContent.ReturnColumnX(), griContent.WidestGridX);
            colContent.AdjustX(bestColXs, roOffsetX, lineLevel[0]);
            griContent.AdjustX(bestColXs, roOffsetX, lineLevel[0]);
            //needs to take in the standard height - single line
            optContent.AdjustX(lineLevel[0]);

            //prepare heights
            float colOffsetY = colContent.AdjustY(lineLevel);
            List<float> bestRowYs = OptimizedFloatsY(lineLevel, roContent.ReturnLineCount(), griContent.ReturnOptLineCount);
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
