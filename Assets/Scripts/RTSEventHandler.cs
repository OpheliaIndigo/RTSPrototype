using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSEventHandler : MonoBehaviour
{

    protected Ray ray;
    protected RaycastHit hit;

    public Vector3 dragStart;
    public Vector3 dragMouseStart;
    public Vector3 dragEnd;
    public Vector3 dragMouseEnd;

    bool dragging;
    public int selectionIncrement = 0;
    public int currentSelection = 0;
    int selectionStarted = 0; // 0 nothing happened, 1 nonshit new selection, 2 shift remove from selection, 3 shift add to selection

    public List<Selectable> selected = new List<Selectable>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!dragging)
        {
            selectionStarted = 0;
        }

        RightMouseButtonHitEvent();
        LeftButtonDragEvent();
        GetBoxSelected();
    }

    void RightMouseButtonHitEvent()
    {
        if (Input.GetMouseButton(HotkeyHandler.mouseButtonMove))
        {
            if (Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < selected.Count; i++)
                {
                    Selectable sel = selected[i];
                    sel.moveToPosition(hit.point);
                }

            }
        }
    }

    void LeftButtonDragEvent()
    {
        if (!Input.GetMouseButton(HotkeyHandler.mouseButtonSelect))
        {
            dragging = false;
            dragStart = Vector3.negativeInfinity;
            dragEnd = Vector3.negativeInfinity;

            dragMouseStart = Vector3.negativeInfinity;
            dragMouseEnd = Vector3.negativeInfinity;

        }
        else if (!dragging)
        {
            // Detect mouse down
            if (Input.GetMouseButtonDown(HotkeyHandler.mouseButtonSelect))
            {
                dragging = true;
                selectionIncrement++;
                dragStart = GetMousePosition();
                dragMouseStart = Input.mousePosition;
            }
        }
        else
        {
            // Detect mouse up, select everything in dragStart, dragEnd area
            if (dragStart.x == Vector3.negativeInfinity.x)
            {
                dragStart = GetMousePosition();
                dragMouseStart = Input.mousePosition;
            }
            dragEnd = GetMousePosition();
            dragMouseEnd = Input.mousePosition;

        }
    }

    void GetBoxSelected()
    {
        if (dragging && dragMouseStart != Vector3.negativeInfinity && dragMouseEnd != Vector3.negativeInfinity)
        {
            // Throw a raycastbox down with center at middle box position raycasted along camera rotation
            Vector3 dragDims = new Vector3(
                Mathf.Abs(dragEnd.x - dragStart.x)/2, 
                Mathf.Abs(dragEnd.y - dragStart.y)/2, 
                Mathf.Abs(dragEnd.z - dragStart.z)/2
            );
            if (dragDims.y <= 0)
            {
                dragDims.y = 0.5f;
            }
            RaycastHit[] hits = Physics.BoxCastAll(SelectionCenterPosition(), dragDims, Quaternion.Inverse(Camera.main.transform.rotation).eulerAngles);
            print("shooting from " + SelectionCenterPosition().ToString() + " width " + dragDims.ToString());
            int selectedThings = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                // Check if i == 0, clear all selections before selecting a thing.
                
                Selectable sel = hits[i].collider.gameObject.GetComponent<Selectable>();
                if (sel != null)
                {
                    if (selectedThings == 0 && !Input.GetKey(HotkeyHandler.addRemoveSelectionKey))
                    {
                        DeselectAll();
                    }
                    SelectThing(sel);
                    selectedThings++;
                }
            }
        }
    }

    void SelectThing(Selectable sel)
    {
        if (selectionStarted == 0) // 0 nothing happened, 1 nonshit new selection, 2 shift remove from selection, 3 shift add to selection
        {
            if (!Input.GetKey(HotkeyHandler.addRemoveSelectionKey)) { selectionStarted = 1; DeselectAll(); } // Realistically want to deselect all and start new selection.
            else if (selected.Contains(sel)) { selectionStarted = 2; }
            else { selectionStarted = 3; }
        }
        switch (selectionStarted)
        {
            case 1:
            case 3:
                sel.Select();
                selected.Add(sel);
                break;
            case 2:
                sel.Deselect();
                selected.Remove(sel);
                break;
        }
    }

    Vector3 SelectionCenterPosition()
    {
        if (dragMouseEnd.x > Vector3.negativeInfinity.x && dragMouseStart.x > Vector3.negativeInfinity.x)
        {
            Ray ray = Camera.main.ScreenPointToRay(Vector3.Lerp(dragMouseStart, dragMouseEnd, 0.5f));
            if (Physics.Raycast(ray, out hit))
            {
                return hit.point;
            }
            
        }
        return Vector3.negativeInfinity;
    }

    void DeselectAll()
    {
        foreach (var sel in selected)
        {
            sel.Deselect();
        }
        selected.Clear();
    }

    private void OnGUI()
    {
        if (dragging && dragEnd != null && dragStart != null)
        {
            Rect rect = Utils.GetScreenRect(dragMouseStart, dragMouseEnd);
            Utils.DrawScreenRectBorder(rect, 3f, Color.black);
        }
    }



    Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.negativeInfinity;

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



}

