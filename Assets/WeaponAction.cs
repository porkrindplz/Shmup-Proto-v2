using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAction : MonoBehaviour
{
    public void Shoot(GameObject bullet, GameObject gunPos, float projectileSpeed)
    {
        GameObject spawn = Instantiate(bullet, gunPos.transform.position, gunPos.transform.rotation);
        spawn.GetComponent<ProjectileBasic>().projectileSpeed = projectileSpeed;
    }
    /*public void LaunchChild(GameObject bullet, GameObject gunPos, float projectileSpeed)
    {
        bullet.GetComponent<ProjectileBasic>().projectileSpeed = projectileSpeed;
    }*/
}
