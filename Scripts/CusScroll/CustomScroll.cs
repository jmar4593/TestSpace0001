using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class CustomScroll : MonoBehaviour
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

        if(Input.GetKeyDown(KeyCode.V))
        {
            SetToPrefer();
        }

        Debug.Log(colContent.GetComponent<RectTransform>().rect.height / this.gameObject.GetComponent<RectTransform>().rect.height);
    }

    private void ExecutePreferred(bool allObjsPrefd, float tenPercentHeight)
    {
        if(allObjsPrefd)
        {
            //prepare widths
            float roOffset = roContent.AdjustX();
            float colOffset = tenPercentHeight;
            optContent.TurnOptions(optionsOn, colOffset);
            colContent.AdjustX(OptimizedWidths(), roOffset, colOffset);
            griContent.AdjustX(OptimizedWidths(), roOffset, colOffset);
            
            //prepare heights

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


    private List<float> OptimizedWidths()
    {
        List<float> optFloats = colContent.ReturnColumnX();

        for(int a = 0; a < optFloats.Count; a++)
        {
            if (optFloats[a] < griContent.CapGridX(a))
            {
                optFloats[a] = griContent.CapGridX(a);

            }

            if (optFloats[a] > 250) optFloats[a] = 250;

        }

        return optFloats;

    }



}
