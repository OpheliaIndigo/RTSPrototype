using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    public int selectionResolution;

    public Material selectionMaterial;

    RaycastHit hit;

    SelectionRect selectionRect;

    Vector3 dragStart;
    Vector3 dragEnd;
    bool dragging;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<LineRenderer>().material = selectionMaterial;
        gameObject.GetComponent<LineRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        DetectMouseDrag();
    }



    void SelectObjects()
    {
        selectionRect = new SelectionRect(dragStart, dragEnd, gameObject);
    }

    void DetectMouseDrag()
    {
        if (!dragging)
        {
            // Detect mouse down
            if (Input.GetMouseButtonDown(0))
            {
                dragging = true;
                dragStart = GetMousePosition();
            }
        } else
        {
            gameObject.GetComponent<LineRenderer>().enabled = true;
            // Detect mouse up, select everything in dragStart, dragEnd area
            SelectObjects();
            dragEnd = GetMousePosition();
            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
            } 
        }
    }

    Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;

    }
}
