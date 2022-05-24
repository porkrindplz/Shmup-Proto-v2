using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class fontCustomSettings : MonoBehaviour
{
    [SerializeField]
    private float outlineWidth;

    [SerializeField]
    private Color outlineColor;

    void Awake()
    {
        TMP_Text textmeshPro = GetComponent<TMP_Text>();


        textmeshPro.outlineWidth = outlineWidth;
        textmeshPro.outlineColor = outlineColor;
        //textmeshPro.outlineColor = new Color32(255, 128, 0, 255);
    }

}
