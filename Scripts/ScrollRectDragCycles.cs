using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectDragCycles : MonoBehaviour
{
    [SerializeField]
    private GameObject scrollContent;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject contentObject;
    private bool disableOnce;
    private bool nextPrevObjectReadied;

    private bool bottomTerminationPoint;
    private bool topTerminationPoint;
    private bool nextObjectExists;
    private bool prevObjectExists;

    private Vector3 topOfScrollView;
    private Vector3 bottomOfScrollView;

    private Vector3 topOfContent;
    private Vector3 bottomOfContent;

    private float directToScalarModifier;
    private int[] topAndBottom;
    private bool topAndBottomLoaded;
    private List<string> fileNames;

    private bool objectsCycled;
    private float oldCursorPosition;

    private float oldContentPosition;

    private List<List<string>> Assets;

    // Update is called once per frame

    private void Awake()
    {
        disableOnce = false;
        objectsCycled = false;
        directToScalarModifier = Screen.height / canvas.GetComponent<RectTransform>().rect.height;
        oldCursorPosition = Input.mousePosition.y;
        oldContentPosition = contentObject.transform.position.y;
        topAndBottomLoaded = false;
        

    }
    void Update()
    {
        if(!disableOnce)
        {
            scrollContent.SetActive(false);
            scrollContent.SetActive(true);
        }
        disableOnce = true;

        if(!topAndBottomLoaded)
        {
            //load element int from objects    
            topAndBottom = new int[] { int.Parse(contentObject.transform.GetChild(0).name), int.Parse( contentObject.transform.GetChild(14).name ) };
            topAndBottomLoaded = true;
            Debug.Log($"Calling out my top element as {topAndBottom[0]} and my bottom element as {topAndBottom[1]} out of {fileNames.Count} names");

        }




        //hit method that checks for object cycling, primary cycling must be called before setting the content to the cursor
        Cycle();


        //hit method that contentObjects to cursorPos.y this might be the root cause of the content object incorrectly positioning
        //itself to the top
        ContentToCursor();

        if (Input.GetMouseButtonUp(0))
        {
            //if mouse up, then reset the script...
            disableOnce = false;
            nextPrevObjectReadied = false;
            topAndBottomLoaded = false;
            //... and disable the entire object.
            this.gameObject.SetActive(false);
        }
    }

    private void ContentToCursor()
    {
        if(objectsCycled == true)
        {
            oldCursorPosition = Input.mousePosition.y;
            oldContentPosition = contentObject.transform.position.y;
            objectsCycled = false;

        }

        //confirmed this is getting hit, however content does not move for some reason
        contentObject.transform.position = new Vector3(contentObject.transform.position.x, oldContentPosition + (Input.mousePosition.y - oldCursorPosition), contentObject.transform.position.z);
    }

    private void IncreaseTopAndBottom()
    {
        topAndBottom[0] = topAndBottom[0] + 1;
        topAndBottom[1] = topAndBottom[1] + 1;
    }
    private void DecreaseTopAndBottom()
    {
        topAndBottom[0] = topAndBottom[0] - 1;
        topAndBottom[1] = topAndBottom[1] - 1;
    }

    //cycle has to check on what position of content that this script got called, because it has to right away have that next/prev object ready to go.
    //This portion of the script, just like the hitOnce check, to disable the ScrollView, can only be hit once. If not, it will continue pooling the objects,
    //infinitely regardless of any checks.

    //Secondly, it must have a check, to ready the following prev/next object to pool.
    private void Cycle()
    {
        //these are moving variables and must be called per method call
        topOfScrollView = scrollContent.transform.position;
        bottomOfScrollView = new Vector3(topOfScrollView.x, topOfScrollView.y - directToScalarModifier * scrollContent.GetComponent<RectTransform>().rect.height, topOfScrollView.z);
        topOfContent = contentObject.transform.position;
        bottomOfContent = new Vector3(topOfContent.x, contentObject.transform.GetChild(14).transform.position.y - directToScalarModifier * contentObject.transform.GetChild(14).GetComponent<RectTransform>().rect.height, topOfContent.z);

        //these are moving variables and must be called per method call
        bottomTerminationPoint = bottomOfContent.y > bottomOfScrollView.y - 10f;
        topTerminationPoint = topOfContent.y < topOfScrollView.y + 10f;
        nextObjectExists = (topAndBottom[1] + 1) < fileNames.Count;
        prevObjectExists = (topAndBottom[0] - 1) >= 0;

        float compensatedHeight;

        //this is the primary readied check. the inside of code can only be called once.
        if (!nextPrevObjectReadied)
        {

            //figure out what needs to be readied. The top content object or the bottom. We can use the current position of the termination points.
            if(bottomTerminationPoint && nextObjectExists)
            {
                //how do we cycle? Content must compensate for the loss of the child(0) position, and move down its equal height. So before cycling this object
                //we need to record its current height
                compensatedHeight = contentObject.transform.position.y - contentObject.transform.GetChild(0).GetComponent<RectTransform>().rect.height * directToScalarModifier;
                //load next object with the correct info
                IncreaseTopAndBottom();
                contentObject.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = fileNames[topAndBottom[1]];
                //load asset quantity
                contentObject.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = Assets[topAndBottom[1]].Count.ToString();
                //load element number to object name
                contentObject.transform.GetChild(0).name = topAndBottom[1].ToString();
                //cycle the child
                contentObject.transform.GetChild(0).SetAsLastSibling();
                //compensate the content transform
                contentObject.transform.position = new Vector3(contentObject.transform.position.x, compensatedHeight, contentObject.transform.position.z);
                //I checked the positioning of the  contentObject transform, and the code is being used correctly. Something is zeroing its position to a home position
                // after this code gets called
                objectsCycled = true;



            }
            if (topTerminationPoint && prevObjectExists)
            {

                compensatedHeight = contentObject.transform.position.y + contentObject.transform.GetChild(14).GetComponent<RectTransform>().rect.height * directToScalarModifier;

                DecreaseTopAndBottom();
                contentObject.transform.GetChild(14).GetChild(0).GetComponent<TextMeshProUGUI>().text = fileNames[topAndBottom[0]];
                contentObject.transform.GetChild(14).GetChild(1).GetComponent<TextMeshProUGUI>().text = Assets[topAndBottom[0]].Count.ToString();
                contentObject.transform.GetChild(14).name = topAndBottom[0].ToString();
                contentObject.transform.GetChild(14).SetAsFirstSibling();
                contentObject.transform.position = new Vector3(contentObject.transform.position.x, compensatedHeight, contentObject.transform.position.z);
                objectsCycled = true;
            }


        }
        nextPrevObjectReadied = true;

        //need to place iterating object cycling here once positioning gets steadied

    }

    /// <summary>
    /// Loads my fileName list;
    /// </summary>

}
