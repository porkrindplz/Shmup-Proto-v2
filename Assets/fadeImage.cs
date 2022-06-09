using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class fadeImage : MonoBehaviour
{

    private Image img;
    private TextMeshPro txt;

    [SerializeField]
    private float alphaTarget;

    [SerializeField]
    private float fadeSpeed;

    [SerializeField]
    private bool fadeImg;

    [SerializeField]
    private bool fadeTxt;
    [SerializeField]
    private bool delay;

    [SerializeField]
    private float delayLength;


    private void Awake()
    {
        img = GetComponent<Image>();
        txt = GetComponent<TextMeshPro>();
        if (delay)
        {
            StartCoroutine("DelayTimer", delayLength);
            delay = false;
        }
    }

    private void Update()
    {
        if (PlayerController.restartCount> 0 || fadeImg && img.color.a == 0)
        {
            Destroy(gameObject);
        }
        if (fadeImg && img != null && !delay) {
            img.CrossFadeAlpha(alphaTarget, fadeSpeed, false);
        }
        if(fadeTxt && txt != null && !delay)
        {
            txt.CrossFadeAlpha(alphaTarget, fadeSpeed, false);
        }
    }

    IEnumerator DelayTimer(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
