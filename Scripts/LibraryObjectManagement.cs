using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class LibraryObjectManagement : MonoBehaviour
{
    public GameObject scrollRectDragCycles;

    private bool beingDragged;


    private bool manualDragging;

    float oldPosition;

    private float compensateDistance;

    private Vector3 topOfScrollView;
    private Vector3 bottomOfScrollView;
    private RectTransform scrollRecTransfPos;
    private Vector3 topOfContent;
    private Vector3 bottomOfContent;
    private RectTransform contRecTrans;

    private int[] topAndBottom;
    private bool topAndBottomLoaded;

    private List<List<string>> Assets;

    private List<string> fileNames;


    public GameObject redDot;
    public GameObject blueDot;
    public GameObject yellowDot;
    public GameObject greenDot;


    private float directToScalarModifier;

    private bool bottomTerminationPoint;
    private bool topTerminationPoint;
    private bool nextObjectExists;
    private bool prevObjectExists;

    

    // Start is called before the first frame update
    void Awake()
    {
        Assets = new List<List<string>>();
        topAndBottom = new int[] { 0, 14 };
        topAndBottomLoaded = true;
        Fill();
        directToScalarModifier = Screen.height / this.transform.parent.parent.parent.parent.GetComponent<RectTransform>().rect.height;
    }


    private void TakeFromTop()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void TakeFromBottom()
    {
        this.transform.GetChild(14).gameObject.SetActive(false);
    }

    private void AddToBottom()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(0).SetAsLastSibling();
    }

    private void AddToTop()
    {
        this.transform.GetChild(14).gameObject.SetActive(true);
        this.transform.GetChild(14).SetAsFirstSibling();
    }

    public void BeginTheDragging()
    {
        beingDragged = true;
    }

    public void StoppedTheDragging()
    {
        beingDragged = false;
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



    /// <summary>
    /// Populate your user list, and sync it with your list of gameobjects
    /// </summary>
    private void Fill()
    {

        for (int a = 0; a < 15; a++)
        {
            GameObject newLDO = this.transform.GetChild(a).gameObject;
            newLDO.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = fileNames[a];
            newLDO.name = a.ToString();
            newLDO.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Assets[a].Count.ToString();

        }

        

    }

    //the pooling portion of this method cannot be called while the scrollrectdragcycles is being called
    public void Cycle()
    {
        //these are moving variables and must be called per method call
        topOfScrollView = this.transform.parent.parent.position;
        bottomOfScrollView = new Vector3(topOfScrollView.x, topOfScrollView.y - directToScalarModifier * this.transform.parent.parent.GetComponent<RectTransform>().rect.height, topOfScrollView.z);
        topOfContent = this.transform.position;
        bottomOfContent = new Vector3(topOfContent.x, this.transform.GetChild(14).transform.position.y - directToScalarModifier * this.transform.GetChild(14).GetComponent<RectTransform>().rect.height, topOfContent.z);

        //these are moving variables and must be called per method call
        bottomTerminationPoint = bottomOfContent.y > bottomOfScrollView.y - 10f;
        topTerminationPoint = topOfContent.y < topOfScrollView.y + 10f;
        nextObjectExists = (topAndBottom[1] + 1) < fileNames.Count;
        prevObjectExists = (topAndBottom[0] - 1) >= 0;


        redDot.transform.position = topOfContent;
        blueDot.transform.position = bottomOfContent;
        greenDot.transform.position = topOfScrollView;
        yellowDot.transform.position = bottomOfScrollView;

        if (!topAndBottomLoaded)
        {
            //load element int from objects    
            topAndBottom = new int[] { int.Parse(this.transform.GetChild(0).name), int.Parse(this.transform.GetChild(14).name) };
            topAndBottomLoaded = true;
        }


        float positionModifier;

        //this if check is only passed if the scrollrectdragcycles object is not active
        if (bottomTerminationPoint && nextObjectExists && !scrollRectDragCycles.activeSelf)
        {

            positionModifier = this.transform.position.y - directToScalarModifier * this.transform.GetChild(0).GetComponent<RectTransform>().rect.height;

            //adjust child directory
            IncreaseTopAndBottom();
            //load new name to child
            this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = fileNames[topAndBottom[1]] ;
            //load new asset quantity to child
            this.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = Assets[topAndBottom[1]].Count.ToString();
            //give new element object name to child
            this.transform.GetChild(0).name = topAndBottom[1].ToString();
            //move the modified child
            this.transform.GetChild(0).SetAsLastSibling();
            //force the content object into the correct position
            this.transform.position = new Vector3(this.transform.position.x, positionModifier, this.transform.position.z);

        }

        if (topTerminationPoint && prevObjectExists && !scrollRectDragCycles.activeSelf)
        {


            positionModifier = this.transform.position.y + directToScalarModifier * this.transform.GetChild(14).GetComponent<RectTransform>().rect.height;

            DecreaseTopAndBottom();
            this.transform.GetChild(14).GetChild(0).GetComponent<TextMeshProUGUI>().text = fileNames[topAndBottom[0]];
            this.transform.GetChild(14).GetChild(1).GetComponent<TextMeshProUGUI>().text = Assets[topAndBottom[0]].Count.ToString();
            this.transform.GetChild(14).name = topAndBottom[0].ToString();
            this.transform.GetChild(14).SetAsFirstSibling();
            this.transform.position = new Vector3(this.transform.position.x, positionModifier, this.transform.position.z);
        }

    }
    
    /// <summary>
    /// This gets attached to the event trigger for ScrollView called "Drag"
    /// </summary>
    public void ManuallyCycle()
    {
        //these are moving variables and must be called per method call
        topOfScrollView = this.transform.parent.parent.position;
        bottomOfScrollView = new Vector3(topOfScrollView.x, topOfScrollView.y - directToScalarModifier * this.transform.parent.parent.GetComponent<RectTransform>().rect.height, topOfScrollView.z);
        topOfContent = this.transform.position;
        bottomOfContent = new Vector3(topOfContent.x, this.transform.GetChild(14).transform.position.y - directToScalarModifier * this.transform.GetChild(14).GetComponent<RectTransform>().rect.height, topOfContent.z);

        //these are moving variables and must be called per method call
        bottomTerminationPoint = bottomOfContent.y > bottomOfScrollView.y - 10f;
        topTerminationPoint = topOfContent.y < topOfScrollView.y + 10f;
        nextObjectExists = (topAndBottom[1] + 1) < fileNames.Count;
        prevObjectExists = (topAndBottom[0] - 1) >= 0;

        if (!topAndBottomLoaded)
        {
            //load element int from objects    
            topAndBottom = new int[] { int.Parse(this.transform.GetChild(0).name), int.Parse(this.transform.GetChild(14).name) };
            topAndBottomLoaded = true;
        }

        if (bottomTerminationPoint && nextObjectExists || topTerminationPoint && prevObjectExists)
        {
            //This sets up the following script to take care of the scrolling and cycling. This LDO script CANNOT interfere with it, so though this script must be active
            // on the "values changed" portion of the Scroll View, in order to take advantage of the fancy inertia scrolling, it must get out of the way for the 
            //ScrollRectDragCycles script.
            topAndBottomLoaded = false;
            scrollRectDragCycles.SetActive(true);

        }

    }


    
}
