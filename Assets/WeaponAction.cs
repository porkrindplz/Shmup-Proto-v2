using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAction : MonoBehaviour
{
    public void Shoot(GameObject bullet, GameObject gunPos)
    {
        Instantiate(bullet, gunPos.transform.position, gunPos.transform.rotation);
    }
}
