using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateLineRenderer : MonoBehaviour
{
    private LineRenderer beam;
    private Vector3 startPos;
    private float endPos;
    private int maxLength = 10;
    private float beamSpeed = 0.3f;
    private float finalWidth = 0.55f;
    private float widthSpeed = 0.001f;
    private EnemyControls enemyControls;
    private float countdownTime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        beam = GetComponent<LineRenderer>();
        enemyControls = GetComponentInParent<EnemyControls>();
        enemyControls.gunActive = false;
        beam.SetPosition(1, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        startPos = transform.parent.position;
        float currentDist = Vector3.Distance(beam.GetPosition(0), beam.GetPosition(1));
        Vector3 lerpDist = Vector3.Lerp(beam.GetPosition(1), new Vector3(startPos.x, startPos.y, startPos.z + maxLength), 1);

        beam.SetPosition(0,startPos);


        var t = Time.time;
        //beam.SetPosition(1, new Vector3(startPos.x, startPos.y, startPos.z + maxLength));

        if (currentDist < maxLength){
            beam.SetPosition(1, lerpDist);
            //beam.SetPosition(1, Vector3.forward *beamSpeed * Time.deltaTime);
            //beamSpeed += beamSpeed;
        }
        if (beam.startWidth < finalWidth)
        {
            beam.startWidth = beam.startWidth + (widthSpeed * Time.deltaTime);
            beam.endWidth = beam.startWidth + (widthSpeed * Time.deltaTime);
            widthSpeed += widthSpeed;
        }
        if (beam.startWidth >= finalWidth && currentDist >= maxLength)
        {
            StartCoroutine("FireCountdown", countdownTime);

        }
    }
    IEnumerator FireCountdown(float time)
    {
        yield return new WaitForSeconds(time);
        enemyControls.isMoving = true;
    }
}
