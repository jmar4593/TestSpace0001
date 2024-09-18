using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LineCount : MonoBehaviour
{
    bool zeroStated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) ThisMethod();
        if (Input.GetKeyDown(KeyCode.R)) Debug.Log(this.GetComponent<RectTransform>().rect);
        NaturalizedWidth();
    }

    private void ThisMethod()
    {
        Debug.Log($"The dim before setting the obj to its default preferred is {this.GetComponent<RectTransform>().rect}");
        //this didnt work on its own, gonna try resetting the grandparent contentSizeFitter. I want this disabled, so that the obj
        //will return its natural width
        //----this.GetComponent<LayoutElement>().enabled = false;
        //reset the grandparent contentsizefitter so that the obj expands to its natural width based on its content
        //----this.transform.parent.parent.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        //----this.transform.parent.parent.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        //this is not returning the naturalized width that the obj is showing now in its rect transform inspector.
        //----this.GetComponent<LayoutElement>().enabled = true;
        this.GetComponent<LayoutElement>().preferredWidth = 0;
        zeroStated = true;

    }

    private bool ConfirmZeroState()
    {
        bool zeroState;
        if (!(this.GetComponent<RectTransform>().rect.width == 0f)) zeroState = false;
        else
        {
            zeroState = true;
        }
        return zeroState;

    }

    private void NaturalizedWidth()
    {
        if(ConfirmZeroState() && zeroStated == true)
        {
            Debug.Log($"The dim after setting the obj to it's zero-state preferred is {this.GetComponent<RectTransform>().rect}");

            this.GetComponent<LayoutElement>().enabled = false;
            this.transform.parent.parent.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            this.transform.parent.parent.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            zeroStated = false;

            //wait until the rect returns a non-zero state value

        }

    }

    private bool ConfirmNonZeroState()
    {
        bool nonZeroState;
        if (!(this.GetComponent<RectTransform>().rect.width != 0f)) nonZeroState = false;
        else
        {
            nonZeroState = true;
        }
        return nonZeroState;
    }


    
}
