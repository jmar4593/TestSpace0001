using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class TextObjFont : MonoBehaviour
{
    [SerializeField]
    private int lines;

    [SerializeField]
    private int fontSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LinesAllowed(lines);
        AdjustFontSize(fontSize);
    }

    private void LinesAllowed (int lines)
    {
        GameObject[] textObjs = GameObject.FindGameObjectsWithTag("TextObj");
        foreach (GameObject textOs in textObjs) textOs.GetComponent<TextMeshProUGUI>().maxVisibleLines = lines;
    }

    private void AdjustFontSize(int fontSize)
    {
        GameObject[] textObjs = GameObject.FindGameObjectsWithTag("TextObj");
        foreach(GameObject textOs in textObjs) textOs.GetComponent<TextMeshProUGUI>().fontSize = fontSize;
    }
}
