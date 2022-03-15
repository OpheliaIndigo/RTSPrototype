using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movable : Selectable
{
    public float height;

    // Update is called once per frame

    public override void moveToPosition(Vector3 newPosition)
    {
        rally = newPosition;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        newPosition.y += height;
        // transform.position = newPosition;
        agent.destination = newPosition;
    }

}
