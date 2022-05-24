using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineRendererAttack : MonoBehaviour
{
    [SerializeField]
    private Transform startPoint;

    [SerializeField]
    private Transform endPoint;

    private LineRenderer line;
    private RaycastHit hit;
    private Transform player;
    private PlayerController playerController;
    private Collider _parentCollider;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        playerController = player.gameObject.GetComponent<PlayerController>();
        _parentCollider = transform.parent.GetComponent<Collider>();

        line.positionCount = 2;

    }

    // Update is called once per frame
    void Update()
    {
        
        line.SetPosition(0, startPoint.position);
        line.SetPosition(1, endPoint.position);
        if (transform != null && player !=null)
        {
            Vector3 rayStart = new Vector3(transform.position.x, player.position.y, transform.position.z);
            float maxDistance = Vector3.Distance(startPoint.position, endPoint.position);
            if (Physics.Raycast(rayStart, transform.TransformDirection(Vector3.forward), out hit, maxDistance, 3))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.white);
                playerController.TriggerHit(transform.parent.GetComponent<Collider>());
            }
        }


    }
}
