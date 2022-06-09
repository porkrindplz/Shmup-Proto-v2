using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeOnDistance : MonoBehaviour
{
    public GameObject target;
    public float triggerDistance;
    private float dist;
    private bossController bossController;
    private bool landingComplete = false;
    public float landingSpeed;
    public float yOffset;
    private Animator anim;

    public float countDown = 2;

    private void Awake()
    {
        bossController = GetComponent<bossController>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = target.transform.position; 
        dist = Mathf.Abs(currentPos.z - targetPos.z);
        if(transform.position.y == 2.1f && !landingComplete)
        {
            landingComplete = true;   
        }

        if (dist <= triggerDistance && !bossController.enemyActive)
        {
            bossController.enemyActive = true;
        } else if (bossController.enemyActive && !landingComplete)
        {
            transform.position = Vector3.MoveTowards(currentPos, new Vector3(currentPos.x, 2.1f, currentPos.z),landingSpeed *Time.deltaTime);
        }
        if (landingComplete && countDown > 0)
        {
            countDown -= Time.deltaTime;

        }else if (countDown <= 0 && anim.enabled == false)
        {
            anim.enabled = true;
        }
    }
}
