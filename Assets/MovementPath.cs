using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    [SerializeField]
    public float speed;

    public bool isMoving;

    private CharacterMovement characterMovement;

    // Start is called before the first frame update
    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            characterMovement.MoveForward(speed);
        }
    }
}
