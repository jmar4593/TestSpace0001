using ES3Types;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;


namespace Scroll
{


    public class Scroll01 : MonoBehaviour
    {
        /// <summary>
        /// Will iterate through the children of the signature object
        /// </summary>
        /// <param name="gameOb"></param>
        public void PreferDims(GameObject gameOb)
        {
            for (int a = 0; a < gameOb.transform.childCount; a++)
            {
                //zero out; loose to overflow to ready for height capping; and set dims to prefer
                gameOb.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                gameOb.transform.GetChild(a).GetComponent<TextMeshProUGUI>().overflowMode = TextOverflowModes.Overflow;
                gameOb.transform.GetChild(a).GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                gameOb.transform.GetChild(a).GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            }


        }


        /// <summary>
        /// After setting textObjs to Preferred, this method returns a bool based on whether the size change has yet occurred.
        /// Had to do this because size changes for ContentSizeFitter, seem to happen very last, even when scripting their
        /// changes in front.
        /// </summary>
        /// <returns></returns>
        public bool ObjsPrefd(GameObject gamo)
        {
            bool prefd = true;

            for (int a = 0; a < gamo.transform.childCount; a++)
            {
                if ((gamo.transform.GetChild(a).GetComponent<TextMeshProUGUI>().text != "") && (gamo.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta == new Vector2(0, 0)))
                {
                    prefd = false;
                }
            }

            return prefd;
        }

        public void OffsetRowOptionBorders(GameObject colOrGrid, float offsetRow, float offsetOpt)
        {
            colOrGrid.transform.parent.GetComponent<RectTransform>().offsetMin = new Vector2(offsetRow, 0);
            colOrGrid.transform.parent.GetComponent<RectTransform>().offsetMax = new Vector2(-offsetOpt, 0);
        }

        public void OffsetColBorder(GameObject roGriOpt, float offsetHt)
        {
            roGriOpt.transform.parent.GetComponent<RectTransform>().offsetMax = new Vector2(roGriOpt.transform.parent.GetComponent<RectTransform>().offsetMax.x, -offsetHt);
        }

        
    }


}


