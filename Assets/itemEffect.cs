using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemEffect : MonoBehaviour
{

    private PlayerController playerController;

    //FlashColor
    Color[] originalColor;
    public MeshRenderer[] meshRenderer;
    private Transform[] children;

    private bool flickerEnabled;
    private float flickerLength = 2f;

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
            for (int i = 0; i < transform.childCount; i++)
            {
                if (children[i].GetComponent<MeshRenderer>() != null)
                {
                    children[i].GetComponent<MeshRenderer>().material.color = Color.green;
                }
            }
            yield return new WaitForSeconds(0.03f);
            for (int i = 0; i < transform.childCount; i++)
            {
                if (children[i].GetComponent<MeshRenderer>() != null)
                {
                    children[i].GetComponent<MeshRenderer>().material.color = originalColor[i];
                }
            }
            yield return new WaitForSeconds(0.03f);
        }
    }
    IEnumerator EnableFlicker(float length)
    {
        flickerEnabled = true;
        yield return new WaitForSeconds(length);
        flickerEnabled = false;
    }

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        originalColor = new Color[playerController.transform.childCount];
        children = new Transform[playerController.transform.childCount];
        for (int i = 0; i < playerController.transform.childCount; i++)
        {
            children[i] = playerController.transform.GetChild(i);
            if (children[i].GetComponent<MeshRenderer>() != null)
            {
                originalColor[i] = children[i].GetComponent<MeshRenderer>().material.color;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !other.transform.name.Contains("Bullet"))
        {
            if (gameObject.name.Contains("Weapon"))
            {
                playerController.ActivateGunSides();
                Invoke("ResetColor", flickerLength);
                DestroyAllItems();
            }
            else if (gameObject.name.Contains("Shield")){
                playerController.shieldActive = true;
                Invoke("ResetColor", flickerLength);
                DestroyAllItems();
            }
        }
    }

    public void DestroyAllItems()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach(GameObject item in items)
        {
            Destroy(item);
        }
    }

}
