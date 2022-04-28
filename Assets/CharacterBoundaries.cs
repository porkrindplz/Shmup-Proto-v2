using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBoundaries : MonoBehaviour
{
    [SerializeField]
    private float xBound = 19.5f;
    [SerializeField]
    private float zBottomBound = 0.5f;
    [SerializeField]
    private float zTopBound = 20.5f;

    private Transform player;
    private Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.localPosition.x > xBound)
        {
            newPos.x = xBound;
        }
        else if (player.localPosition.x < -xBound)
        {
            newPos.x = -xBound;
        } else
        {
            newPos.x = player.localPosition.x;
        }
        if (player.localPosition.z > zTopBound)
        {
            newPos.z = zTopBound;
        }
        else if (player.localPosition.z < -zBottomBound)
        {
            newPos.z = -zBottomBound;
        }
        else
        {
            newPos.z = player.localPosition.z;
        }

        newPos.y = player.localPosition.y;

        player.localPosition = newPos;

    }
}
