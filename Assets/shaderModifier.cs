using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaderModifier : MonoBehaviour
{
    private Renderer rend;
    private bossController boss;
    private float fadeEnd = 0;
    private float newFade=1;
    private float countDown=2;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        boss = FindObjectOfType<bossController>();
    }

    // Update is called once per frame
    void Update()
    {
        float fade = rend.material.GetFloat("Fade");
        if (boss.enemyActive && newFade>0)
        {
            if (countDown > 0)
            {
                countDown -= Time.deltaTime;
            }
            else
            {

                newFade = Mathf.Lerp(fade, fadeEnd, .1f);
                rend.material.SetFloat("Fade", newFade);
            }
        }
    }
}
