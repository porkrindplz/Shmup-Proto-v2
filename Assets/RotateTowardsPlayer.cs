using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private Vector3 playerGroundPos;
    private Vector3 playerDir;

    private float rotationStep;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }


    public void TargetPlayer(float rotationSpeed)
    {
        if (player != null)
        {
            playerGroundPos = new Vector3(player.position.x, player.position.y, player.position.z);
            playerDir = playerGroundPos - transform.position;
            rotationStep = rotationSpeed * Time.deltaTime;
            Vector3 newLookDir = Vector3.RotateTowards(transform.forward, playerDir, rotationStep, 0f);
            transform.rotation = Quaternion.LookRotation(newLookDir);
        }
    }
}
