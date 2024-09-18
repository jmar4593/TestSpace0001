using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Scroll
{
    public class Scroll01 : MonoBehaviour
    {
        /// <summary>
        /// Will iterate through the children of the signature object, and disable the layout element of the children. This will allow the
        /// width of the children to stretch as far as they need horizontally, so they can be compared for bestFit/widthCapped. 
        /// Following methods must confirm when the preferred has actually occured (by counting a line Count of 1), before executing. Is called
        /// from Row, Column, and Grid content files.
        /// </summary>
        /// <param name="gameOb"></param>
        public void GetDefaultPrefer(GameObject gameOb)
        {
            for (int a = 0; a < gameOb.transform.childCount; a++)
            {
                //gameOb.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                gameOb.transform.GetChild(a).GetComponent<TextMeshProUGUI>().overflowMode = TextOverflowModes.Overflow;
                //gameOb.transform.GetChild(a).GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                //gameOb.transform.GetChild(a).GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                gameOb.transform.GetChild(a).GetComponent<LayoutElement>().enabled = false;
            }
        }

        /// <summary>
        /// After setting textObjs to Preferred, this method returns a bool based on whether the size change has yet occurred.
        /// Had to do this because size changes for ContentSizeFitter, seem to happen very last, even when scripting their
        /// changes in front.
        /// </summary>
        /// <param name="gamo"></param>
        /// <returns></returns>
        public bool ObjsPrefd(GameObject gamo)
        {
            bool prefd = true;
            for (int a = 0; a < gamo.transform.childCount; a++)
            {
                if ((gamo.transform.GetChild(a).GetComponent<TextMeshProUGUI>().text != "") && (gamo.transform.GetChild(a).GetComponent<RectTransform>().sizeDelta == new Vector2(0, 0))) prefd = false;
            }
            return prefd;
        }

        /// <summary>
        /// After filling the object with text, the autosize has to make sure that the line count updates to a nonzero. Used in obj groupings, and called from CustomScroll during AdjustY;
        /// </summary>
        /// <param name="gamo"></param>
        /// <returns></returns>
        public bool LineCountNonZero(GameObject gamo)
        {
            bool nonZero = true;
            for(int a = 0; a < gamo.transform.childCount; a++)
            {
                if ((gamo.transform.GetChild(a).GetComponent<TextMeshProUGUI>().text != "") && (gamo.transform.GetChild(a).GetComponent<TextMeshProUGUI>().textInfo.lineCount == 0)) nonZero = false;
            }
            return nonZero;
        }

        /// <summary>
        /// Takes the viewport of the column or grid, and applies the signature offsets from the row and options object.
        /// Called from ColumnContent and OptionsContent.
        /// </summary>
        /// <param name="colOrGrid"></param>
        /// <param name="offsetRow"></param>
        /// <param name="offsetOpt"></param>
        public void OffsetRowOptionBorders(GameObject colOrGrid, float offsetRow, float offsetOpt)
        {
            colOrGrid.transform.parent.GetComponent<RectTransform>().offsetMin = new Vector2(offsetRow, 0);
            colOrGrid.transform.parent.GetComponent<RectTransform>().offsetMax = new Vector2(-offsetOpt, 0);
        }

        /// <summary>
        /// Takes the viewport of the row, or grid, or options, and applies the signature offset from the column object.
        /// Called from the RowContent, GridContent, and OptionsContent.
        /// </summary>
        /// <param name="roGriOpt"></param>
        /// <param name="offsetHt"></param>
        public void OffsetColBorder(GameObject roGriOpt, float offsetHt)
        {
            roGriOpt.transform.parent.GetComponent<RectTransform>().offsetMax = new Vector2(roGriOpt.transform.parent.GetComponent<RectTransform>().offsetMax.x, -offsetHt);
        }
    }
}


