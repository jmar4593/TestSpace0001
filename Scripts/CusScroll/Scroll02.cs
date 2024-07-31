using Scroll;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Scroll02 : MonoBehaviour
{
    public delegate float GetAdjacentGridValues(int gridLineValue);

    /// <summary>
    /// Takes the single dim floats of either the row or column object and compares them
    /// to their adjacent grid dim floats to determine the largest dim (across the line) and return it, into
    /// a list. Called from CustomScroll
    /// </summary>
    /// <param name="mainFloats"></param>
    /// <param name="largestAdjFloat"></param>
    /// <returns></returns>
    public List<float> OptimizedFloatsX(List<float> mainFloats, GetAdjacentGridValues largestAdjFloat)
    {
        List<float> optFloats = mainFloats;

        for (int a = 0; a < optFloats.Count; a++)
        {
            float lgstAdjFlt = largestAdjFloat(a);
            if (optFloats[a] < lgstAdjFlt)
            {
                optFloats[a] = lgstAdjFlt;

            }
            //cap has to happen here
            if (optFloats[a] > 250) optFloats[a] = 250;
        }

        return optFloats;
    }

    public List<float> OptimizedFloatsY(List<float> mainFloats, GetAdjacentGridValues largestAdjFloat)
    {
        List<float> optFloats = mainFloats;

        for (int a = 0; a < optFloats.Count; a++)
        {
            float lgstAdjFlt = largestAdjFloat(a);
            if (optFloats[a] < lgstAdjFlt)
            {
                optFloats[a] = lgstAdjFlt;

            }
            //cap has to happen here

        }
        return optFloats;
    }

    public void CapY(GameObject customScrollObject, float capThisHeight)
    {
        float singleLine = 0.071f * customScrollObject.GetComponent<RectTransform>().sizeDelta.y;
        float doubleLine = 0.107f * customScrollObject.GetComponent<RectTransform>().sizeDelta.y;
        float tripleLine = 0.142f * customScrollObject.GetComponent<RectTransform>().sizeDelta.y;

        customScrollObject.GetComponent<TextMeshProUGUI>().maxVisibleLines = 3;
        for (int a = 0; a < this.transform.childCount; a++)
        {

            if (this.transform.GetChild(a).GetComponent<TextMeshProUGUI>().textInfo.lineCount == 1)
            {
                capThisHeight = singleLine;
            }
            if (this.transform.GetChild(a).GetComponent<TextMeshProUGUI>().textInfo.lineCount == 2)
            {
                capThisHeight = doubleLine;
            }
            if (this.transform.GetChild(a).GetComponent<TextMeshProUGUI>().textInfo.lineCount > 2)
            {
                capThisHeight = tripleLine;
            }

        }
    }
}
