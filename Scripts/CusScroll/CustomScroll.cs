using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomScroll : MonoBehaviour
{
    [SerializeField]
    private ColumnContent colContent;

    [SerializeField]
    private RowContent roContent;

    [SerializeField]
    private GridContent griContent;

    [SerializeField]
    private OptionsContent optContent;


    private List<string> foreignListRow;
    private List<string> foreignListColumn;
    private List<List<string>> foreignListGrid;

    [SerializeField]
    [Tooltip("Turn on to setup options in right corner of listed objects")]
    private bool optionsOn;


    private void Start()
    {

        //optContent.TurnOptions(optionsOn, colContent.ReturnHeight());
        roContent.PreferFitObjects();

    }

    


    /// <summary>
    /// how do coroutines work? they might be able to replace update needs. the way i think it works is that it can be placed in a method
    /// and setup to run for a specified amount of time. the coroutine can be run by an event trigger attached to the same object as this script
    /// </summary>
    private void Update()
    {
        //roContent.SetWidth();
        //colContent.CheckColBoundary(roContent.ReturnWidth(), optContent.ReturnWidth());
        //griContent.CheckGridBoudaries(roContent.ReturnWidth(), optContent.ReturnWidth());
        //griContent.GridSetGroupingObjs(roContent.RowObjHeights(),colContent.colObjWidths());

    }



    // Start is called before the first frame update
    void OldLines()
    {
        /*
        foreignListRow = new List<string> { "string", "gun", "row" };
        roContent.CallRow(foreignListRow);

        foreignListColumn = new List<string> { "twenties is cool", "forward march says the frog", "senshi sensei" };
        colContent.CallColumn(foreignListColumn, roContent.OptimalRowWidth(), optContent.OptimalOptionsWidth(true));

        foreignListGrid = new List<List<string>> { new List<string>{ "deal", "renumeration", "psychodelic" }, new List<string> { "apollyean", "nachos", "ghiardelli"}, new List<string>{"deep pockets", "deep galactic", "dark orbit"} };
        griContent.CallGrid(foreignListGrid, roContent.OptimalRowWidth(), optContent.OptimalOptionsWidth(true));

        optContent.CallOptions(true, foreignListRow.Count);
        */

    }


    public void AdjustHorizontal()
    {
        Invoke("JumpWidthsColumnGrid", 0.1f);
    }
}
