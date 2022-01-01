using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Selectable : MonoBehaviour
{
    public bool selected;
    public float baseRadius;

    public Material baseMaterial;
    public float selectionBaseHeight;

    GameObject selectionCircle;

    protected Ray ray;
    protected RaycastHit hit;


    // Start is called before the first frame update
    public virtual void Start()
    {
        selectionCircle = new GameObject();
        selectionCircle.name = name;
        selectionCircle.transform.parent = transform;
        selectionCircle.transform.position = transform.position;
        SelectionBase.CreateSelectionBaseObject(selectionCircle, selectionBaseHeight, baseMaterial);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (MouseButtonHitEvent(0))
        {
            SelectionEvent();
        }
    }

    protected void UpdateSelectionSprite()
    {
        //selectionBaseInstance.SetActive(selected);
        selectionCircle.SetActive(selected);
    }

    protected bool MouseButtonHitEvent(int mouseButton)
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(mouseButton))
                if (hit.transform.position == this.transform.position)
                {
                    return true;
                }
        }
        return false;
    }

    public void SelectionEvent()
    {
        selected = !selected;
        UpdateSelectionSprite();
    }
}
