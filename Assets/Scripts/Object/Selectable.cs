using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Selectable : MonoBehaviour
{
    public bool selected;
    public float baseRadius;
    public float baseThickness = 0.2f;

    public Material baseMaterial;
    public float selectionBaseHeight;

    GameObject selectionCircle;

    protected Ray ray;
    protected RaycastHit hit;

    public Vector3 rally;


    // Start is called before the first frame update
    public virtual void Start()
    {
        selectionCircle = new GameObject();
        selectionCircle.name = name;
        selectionCircle.transform.localScale = Vector3.one;
        selectionCircle.transform.parent = transform;
        selectionCircle.transform.position = transform.position;
        SelectionBase.CreateSelectionBaseObject(selectionCircle, selectionBaseHeight, baseMaterial, baseRadius, baseThickness);
    }

    // Update is called once per frame
    public void Update()
    {
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

    protected void UpdateSelectionSprite()
    {
        //selectionBaseInstance.SetActive(selected);
        selectionCircle.SetActive(selected);
    }

    public void Select()
    {
        selected = true;
        UpdateSelectionSprite();
    }

    public void Deselect()
    {
        selected = false;
        UpdateSelectionSprite();
    }


    public void SelectionEvent()
    {
        selected = !selected;
        UpdateSelectionSprite();
    }

    public virtual void moveToPosition(Vector3 position) {
        rally = position;
    }
}
