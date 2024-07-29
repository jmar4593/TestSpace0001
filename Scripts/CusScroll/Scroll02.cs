using System.Collections;
using System.Collections.Generic;
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
    /// <param name="adjacentFloat"></param>
    /// <returns></returns>
    public List<float> OptimizedFloats(List<float> mainFloats, GetAdjacentGridValues adjacentFloat)
    {
        List<float> optFloats = mainFloats;

        for (int a = 0; a < optFloats.Count; a++)
        {
            float adjFloat = adjacentFloat(a);
            if (optFloats[a] < adjFloat)
            {
                optFloats[a] = adjFloat;

            }
        }

        return optFloats;

    }
}
