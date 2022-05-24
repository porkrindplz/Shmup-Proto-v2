using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTargeting : MonoBehaviour
{
    [SerializeField]
    private int layer;

    [SerializeField]
    private float maxDistance;

    private int layerTarget;
    private RaycastHit hit;
    private GameObject player;
    private float playerY;
    private EnemyControls enemyControls;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerY = player.transform.position.y;
        layerTarget = 1 << layer;
        enemyControls = GetComponent<EnemyControls>();
        enemyControls.gunActive = false;
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 rayStart = new Vector3(transform.position.x, playerY, transform.position.z);
        if (Physics.Raycast(rayStart, transform.TransformDirection(Vector3.forward), out hit, maxDistance, layerTarget))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            enemyControls.gunActive = true;
        }
        else
        {
            //Debug.DrawRay(rayStart, transform.TransformDirection(Vector3.forward) * 20, Color.yellow);
            enemyControls.gunActive = false;
        }
    }
}
