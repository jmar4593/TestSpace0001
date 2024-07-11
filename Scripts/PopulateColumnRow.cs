using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopulateColumnRow : MonoBehaviour
{
    public GameObject rowContent;
    public GameObject columnContent;


    [SerializeField]
    private List<string> rowValues;

    [SerializeField]
    private List<string> columnValues;



    public void DefineRowAndColumn(List<string> rowVal, List<string> colVal)
    {
        this.rowValues = rowVal;
        this.columnValues = colVal;

        this.rowValues.ForEach(ShoutThis);
        this.columnValues.ForEach(ShoutThis);

        Debug.Log(this.name);

    }

    void ShoutThis(string shout)
    {
        Debug.Log(shout);
    }
}
