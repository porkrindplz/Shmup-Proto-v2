using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float formationPosition;
    private float xMove;

    private void Awake()
    {

            xMove = -formationPosition;
    }
    public void MoveForward(float speed)
    {

        if (gameObject.CompareTag("Player"))
        {
            Vector3 forward = Vector3.forward * speed * Time.deltaTime;
            transform.localPosition += forward;
        }
            else
        {
            Vector3 forward = transform.forward * speed * Time.deltaTime;
            transform.position += forward;
        }

    }    
    public void LocalMoveForward(float speed)
    {
            Vector3 forward = transform.forward * speed * Time.deltaTime;
            transform.localPosition += forward;
    }
    public void MoveLeft(float speed)
    {
        Vector3 left = Vector3.left * speed * Time.deltaTime;
        transform.localPosition += left;
    }
    public void MoveRight(float speed)
    {
        Vector3 right = Vector3.right * speed * Time.deltaTime;
        transform.localPosition += right;
    }
    public void MoveBack(float speed)
    {
        Vector3 back = Vector3.back * speed * Time.deltaTime;
        transform.localPosition += back;
    }

    public void sMovement(float speed)
    { 
        MoveForward(speed);
    }

    public void SinMovement(float speed, Vector3 startPos)
    {
        //Vector3 sinMov = startPos + new Vector3((Mathf.Sin(Time.time)+(startPos.z-transform.position.z)) * speed, 0, 1);
        Vector3 sinMov = startPos + new Vector3(Mathf.Sin(Time.time+xMove) * speed, 0, .5f*speed);
        transform.position = sinMov;
    }
}
