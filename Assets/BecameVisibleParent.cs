using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecameVisibleParent : MonoBehaviour
{
    private bool onScreen;
    private float offScreenTime = 0;
    private float offScreenDeathTime = 4;
    private EnemyControls enemyControls;
    private Animator anim;

    private void Awake()
    {
        enemyControls = gameObject.GetComponentInParent<EnemyControls>();
        anim = gameObject.GetComponentInParent<Animator>();
    }

    private void OnBecameVisible()
    {
        enemyControls.enemyActive = true;
        onScreen = true;
        if(anim != null)
        {
            anim.SetTrigger("isVisible");
        }
    }

    private void Update()
    {
        for (int i = 0; i<3;i++)
        if (gameObject != null && enemyControls.enemyActive && !onScreen&& offScreenTime >= offScreenDeathTime)
        {
                // StartCoroutine("DeathTime", offScreenTime);
                gameObject.GetComponentInParent<EnemyControls>().HasDied();

        } else if(gameObject != null && enemyControls.enemyActive && !onScreen)
            {
                offScreenTime += Time.deltaTime;
            }
        else if (gameObject!= null && enemyControls.enemyActive && onScreen)
            {
                offScreenTime = 0;
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

        }
    }


}
