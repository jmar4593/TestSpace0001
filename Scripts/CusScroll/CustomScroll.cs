using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;


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

    public event Action thisThing;

    private void Happening()
    {

    }

    private void Update()
    {
        //This method stays in preferred, but does not execute until I set a bool to do it.
        AdjustBorders(AllObjsPrefd(),AllLineCountNonzero());
        if (Input.GetKeyDown(KeyCode.V))
        {
            GetDefaultPrefer();
        }
    }

    private void AdjustBorders(bool allObjsPrefd, bool allLineCountNonzero)
    {
        if(allObjsPrefd)
        {
            float[] lineLevel = new float[3];
            lineLevel[0] = 0.071f * this.GetComponent<RectTransform>().rect.height;
            lineLevel[1] = 0.107f * this.GetComponent<RectTransform>().rect.height;
            lineLevel[2] = 0.142f * this.GetComponent<RectTransform>().rect.height;
            Action<float> action = new Action<float>(Dlog);
            Array.ForEach(lineLevel,Dlog);
            AdjustX(allObjsPrefd, lineLevel[0]);

            AdjustY(allLineCountNonzero, lineLevel);
        }
    }

    private void Dlog(float lineLevel)
    {
        Debug.Log(lineLevel);
    }

    private void AdjustX(bool allObjsPrefd, float standardHeight)
    {
        if(allObjsPrefd)
        {
            float roOffsetX = roContent.AdjustX();
            optContent.TurnOptions(optionsOn, standardHeight);
            List<float> bestColXs = OptimizedFloatsX(colContent.ReturnColumnX(), griContent.WidestGridX);
            colContent.AdjustX(bestColXs, roOffsetX, standardHeight);
            griContent.AdjustX(bestColXs, roOffsetX, standardHeight);
            //needs to take in the standard height - single line
            optContent.AdjustX(standardHeight);
        }
    }

    private void AdjustY(bool allLineCountNonzero, float[] lineLevel)
    {
        if(allLineCountNonzero)
        {
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
    private void GetDefaultPrefer()
    {
        roContent.GetDefaultPrefer();
        colContent.GetDefualtPrefer();
        griContent.GetDefualtPrefer();
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

    private bool AllLineCountNonzero()
    {
        bool lineCountNonzero = false;
        if (roContent.LineCountNonzero() && colContent.LineCountNonzero() && griContent.LineCountNonzero()) lineCountNonzero = true;

        return lineCountNonzero;

    }
}
