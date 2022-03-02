using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    public int selectionResolution;

    public Material selectionMaterial;
    public LayerMask selectionMask;

    RaycastHit hit;

    SelectionRect selectionRect;

    Vector3 dragStart;
    Vector3 dragEnd;
    bool dragging;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<LineRenderer>().material = selectionMaterial;
        gameObject.GetComponent<LineRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        gameObject.GetComponent<LineRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        DetectMouseDrag();
    }



    void SelectObjects()
    {
        selectionRect = new SelectionRect(dragStart, dragEnd, gameObject, selectionMask);
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
            dragEnd = GetMousePosition();
            SelectObjects();
            
            if (Input.GetMouseButtonUp(0))
            {
                print(dragStart.ToString());
                print(dragEnd.ToString());
                dragging = false;
                gameObject.GetComponent<LineRenderer>().enabled = false;
            } 
        }
    }

    private void OnGUI()
    {
        if (dragging)
        {
            Rect r = Utils.GetScreenRect(dragStart, dragEnd);
            Utils.DrawScreenRect(r,Color.white);
            print("drawing rect");
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 position = new Vector3(
                    dragStart.x + (dragEnd.x - dragStart.x) / 2,
                    25,
                    dragStart.z + (dragEnd.z - dragStart.z) / 2
                );
        Vector3 scale = new Vector3(
                    (dragEnd.x - dragStart.x) / 2,
                    150,
                    (dragEnd.z - dragStart.z) / 2
                );


        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        if (dragging)
        {
            Gizmos.DrawWireCube(position, scale);
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
