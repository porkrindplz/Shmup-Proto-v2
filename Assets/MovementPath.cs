using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private CharacterMovement characterMovement;

    // Start is called before the first frame update
    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        characterMovement.MoveForward(speed);
    }
}
