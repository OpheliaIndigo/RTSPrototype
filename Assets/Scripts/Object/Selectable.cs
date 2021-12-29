using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Selectable : MonoBehaviour
{
    public bool selected;
    public float baseRadius;

    public GameObject selectionBase;
    private GameObject selectionBaseInstance;

    protected Ray ray;
    protected RaycastHit hit;


    // Start is called before the first frame update
    public virtual void Start()
    {
        selectionBaseInstance = SelectionBase.CreateSelectionBaseObject(selectionBase, this.gameObject);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (MouseButtonHitEvent(0))
        {
            SelectionEvent();
        }
    }

    protected void RecalculateObjectLocationData()
    {
        SelectionBase.UpdateSelectionBaseObject(selectionBaseInstance, this.gameObject);
    }

    protected void UpdateSelectionSprite()
    {
        selectionBaseInstance.SetActive(selected);
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

    protected void SelectionEvent()
    {
        selected = !selected;
        UpdateSelectionSprite();
        print(hit.collider.name + " " + selected.ToString());
    }
}
