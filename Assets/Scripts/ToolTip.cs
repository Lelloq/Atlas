using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{

    public Text toolTipText;
    public RectTransform backgroundRectTransform;
    public GameObject Background;

    public void ShowToolTip(string toolTipString)
    {
        gameObject.SetActive(true);
        Background.SetActive(true);

        toolTipText.text = toolTipString;
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
        Background.SetActive(false);
    }
}
