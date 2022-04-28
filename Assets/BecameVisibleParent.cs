using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecameVisibleParent : MonoBehaviour
{
    private bool onScreen;
    private float offScreenTime;
    private EnemyControls enemyControls;

    private void Awake()
    {
        enemyControls = gameObject.GetComponentInParent<EnemyControls>();
    }

    private void OnBecameVisible()
    {
        enemyControls.enemyActive = true;
        onScreen = true;
    }

    private void Update()
    {
        if (gameObject != null && enemyControls.enemyActive && !onScreen)
        {
            StartCoroutine("DeathTime", offScreenTime);
        }
    }

    private void OnBecameInvisible()
    {
        if (onScreen && gameObject != null)
        {
            onScreen = false;
        }   
    }

    IEnumerator DeathTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (!onScreen)
        {
            gameObject.GetComponentInParent<EnemyControls>().HasDied();
        }
    }


}
