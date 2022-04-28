using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    private Transform cam;
    [SerializeField]
    private float yOffset;
    [SerializeField]
    private float zOffset;

    [SerializeField]
    private float followSpeed;

    [SerializeField]
    private Transform target;

    private MovementPath movementPath;

    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Transform>();
        movementPath = FindObjectOfType<MovementPath>();
    }

    // Update is called once per frame
    void Update()
    {
        target = movementPath.transform;
        if (target != null)
        {
            cam.position = new Vector3(cam.position.x, target.position.y + yOffset, Mathf.Lerp(cam.position.z, target.position.z + zOffset, Time.deltaTime * followSpeed));
        }
    }
}
