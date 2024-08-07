using GameKit.Utilities;
using Scroll;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Scroll02 : MonoBehaviour
{
    public delegate float GetAdjacentGridValsX(int gridLineValue);

    public delegate int GetAdjacentGridValsY(int gridLineValue);

    /// <summary>
    /// Takes the single dim floats of either the row or column object and compares them
    /// to their adjacent grid dim floats to determine the largest dim (across the line) and return it, into
    /// a list. Called from CustomScroll
    /// </summary>
    /// <param name="mainFloats"></param>
    /// <param name="largestAdjFloat"></param>
    /// <returns></returns>
    public List<float> OptimizedFloatsX(List<float> mainFloats, GetAdjacentGridValsX largestAdjFloat)
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

    public List<float> OptimizedFloatsY(float[] lineLevels, List<int> lineCountMains, GetAdjacentGridValsY largestLineCountGridAdj)
    {
        List<float> optSizes = new List<float>();
        for (int a = 0; a < lineCountMains.Count; a++)
        {
            int lgstAdjFlt = largestLineCountGridAdj(a);
            if (lineCountMains[a] < lgstAdjFlt)
            {
                lineCountMains[a] = lgstAdjFlt;

            }
            //cap has to happen here, will return an actual measurement
            optSizes.Add(lineLevels[lineCountMains[a]]);
        }
        return optSizes;
    }

}
