using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        /// This sits in cap methods
        /// </summary>
        /// <returns></returns>
        public bool ObjsPrefd(GameObject gamo)
        {
            bool prefd = true;

            for (int a = 0; a < gamo.transform.childCount; a++)
            {
                if ((gamo.transform.GetChild(a).GetComponent<TextMeshProUGUI>().text != "") && (gamo.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta == new Vector2 (0, 0)))
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



    }
}


