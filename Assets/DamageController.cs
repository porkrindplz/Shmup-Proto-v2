using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    Color[] originalColor;

    public MeshRenderer[] meshRenderer;

    private Transform[] children;
    
    [SerializeField]
    public int damage;

    private bool flashing;
    private bool flickerEnabled;

    private float flashLength = 0.01f;
    private float flickerLength;

    private void Start()
    {
        originalColor = new Color[transform.childCount];
        children = new Transform[transform.childCount];
        for (int i = 0; i<transform.childCount;i++)
        {

                children[i] = transform.GetChild(i);
            if (children[i].GetComponent<MeshRenderer>() != null && children[i].GetComponent<MeshRenderer>().material.HasProperty("_Color"))
            {
                originalColor[i] = children[i].GetComponent<MeshRenderer>().material.color;
            }
        }
        flashing = false;
    }

    private void Update()
    {
        if (flashing)
        {

        }
    }

    public void FlashRed(float flickerTime)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (children[i].GetComponent<MeshRenderer>() != null)
            {
                children[i].GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
        flickerLength = flickerTime - flashLength;
        Invoke("ResetColor", flashLength);
    }

    void ResetColor()
    {
        StartCoroutine("EnableFlicker", flickerLength);
        for (int i = 0; i < transform.childCount; i++)
        {
            if (children[i].GetComponent<MeshRenderer>() != null)
            {
                children[i].GetComponent<MeshRenderer>().material.color = originalColor[i];
            }
        }
        StartCoroutine("Flicker");
    }

    IEnumerator Flicker()
    {
        while (flickerEnabled)
        {
            for (int i = 0; i< transform.childCount; i++) 
            {
                if(children[i].GetComponent<MeshRenderer>() !=null)
                {
                    children[i].GetComponent<MeshRenderer>().material.color = Color.clear;
                }
            }
            yield return new WaitForSeconds(0.02f);
            for (int i = 0; i < transform.childCount; i++)
            {
                if (children[i].GetComponent<MeshRenderer>() != null)
                {
                    children[i].GetComponent<MeshRenderer>().material.color = originalColor[i];
                }
            }
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator EnableFlicker(float length)
    {
        flickerEnabled = true;
        yield return new WaitForSeconds(length);
        flickerEnabled = false;
    }

}
