using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeOnTrigger : MonoBehaviour
{
    public bool isSphereCollider;
    public bool isBoxCollider;

    public GameObject trigger;

    private bossController bossController;

    private void Awake()
    {
        bossController = GetComponent<bossController>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject.transform.parent.gameObject;
        if (isSphereCollider == true && target == trigger && gameObject.GetComponentInChildren<Collider>().GetType() == typeof (SphereCollider))
        {
            bossController.enemyActive = true;
        }
        else if (isBoxCollider == true && target == trigger && gameObject.GetComponentInChildren<Collider>().GetType()==typeof(BoxCollider))
        {
            bossController.enemyActive = true;
        }

    }
}
