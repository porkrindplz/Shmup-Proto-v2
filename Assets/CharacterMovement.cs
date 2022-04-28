using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public void MoveForward(float speed)
    {
        if (gameObject.CompareTag("Player"))
        {
            Vector3 forward = Vector3.forward * speed * Time.deltaTime;
            transform.localPosition += forward;
        }
            else//if (gameObject.name.Contains("Tank"))
        {
            Vector3 forward = transform.forward * speed * Time.deltaTime;
            transform.position += forward;
        }

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
        Vector3 pos = transform.localPosition;
        Vector3 curveRight = Vector3.right;
        MoveForward(speed);
    

    }
}
