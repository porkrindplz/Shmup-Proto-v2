using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastAttack : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    public Transform player;
    public Transform endPoint;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 rayStart = new Vector3(transform.position.x, player.position.y, transform.position.z);
            ray = new Ray(rayStart, transform.forward);
            Debug.DrawRay(transform.position, transform.forward);
            float maxDistance = Vector3.Distance(transform.position, endPoint.position);

            if (Physics.Raycast(ray, out hit, maxDistance) && player != null)
            {
                if (player = hit.transform)
                {
                    Debug.Log("YOUCH");
                    playerController.TakeDamage(1);
                }
            }
        }
    }
}
