using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBasic : MonoBehaviour
{
    [SerializeField] public float projectileSpeed = 20f;

    [SerializeField] private float range = 10;

    private Vector3 startPos;
    private Vector3 currentPos;
    private float playerSpeed;


    private void Start()
    {
        startPos = transform.position;

    }

    void Update()
    {

        if (gameObject.CompareTag("Player") && FindObjectOfType<PlayerController>() != null)
        {
            playerSpeed = FindObjectOfType<PlayerController>().speed;
        }
        transform.Translate(new Vector3(0f, 0f, (projectileSpeed + playerSpeed) * Time.deltaTime));
        currentPos = transform.position;
        if(Vector3.Distance(startPos,currentPos) > range)
        {
            Destroy(gameObject);
        }

    }
}
