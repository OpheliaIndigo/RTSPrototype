using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movable : Selectable
{
    public float height;

    // Update is called once per frame
    public override void Update()
    {
        if (MouseButtonHitEvent(0))
        {
            SelectionEvent();
        }
        if (selected && Input.GetMouseButtonDown(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 newPosition = hit.point;
                moveToPosition(newPosition);
            }
        }
    }

    protected void moveToPosition(Vector3 newPosition)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        newPosition.y += height;
        // transform.position = newPosition;
        agent.destination = newPosition;
    }

}
