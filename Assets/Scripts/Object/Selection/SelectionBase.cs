using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SelectionBase
{
    public static void CreateSelectionBaseObject(GameObject parent, float selectionBaseHeight, Material baseMaterial, float baseRadius, float baseThickness)
    {
        /*GameObject selectionBaseInstance = GameObject.Instantiate(basePrefab);
        selectionBaseInstance.transform.SetParent(parent.transform);

        Vector3 basePosition = calculateBasePosition(parent);
        selectionBaseInstance.transform.position = basePosition;*/
        baseMaterial.renderQueue = (int)RenderQueue.Background;
        CreateCircle(parent, selectionBaseHeight, baseMaterial, baseRadius, baseThickness);

        //return selectionBaseInstance;
    }

    public static void CreateCircle(GameObject parent, float selectionBaseHeight, Material baseMaterial, float baseRadius, float baseThickness)
    {
        if (parent.GetComponent<LineRenderer>() == null)
        {
            parent.AddComponent<LineRenderer>();
        }
        parent.AddComponent<Circle>();
        Circle circle = parent.GetComponent<Circle>();
        circle.segments = 360;
        circle.xradius = circle.zradius = baseRadius;
        circle.yposition = selectionBaseHeight;
        circle.baseThickness = baseThickness;
        LineRenderer lr = parent.GetComponent<LineRenderer>();

        lr.material = baseMaterial;
        lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    public static void UpdateSelectionBaseObject(GameObject selectionBase, GameObject parent)
    {
        selectionBase.transform.position = calculateBasePosition(parent);
    }

    private static Vector3 calculateBasePosition(GameObject parent)
    {

        RaycastHit hit;
        if (Physics.Raycast(parent.transform.position, Vector3.down, out hit))
        {
            Vector3 point = hit.point;
            point.y += 0.1f;
            return point;
        }
        else
        {
            return Vector3.zero;
        }
    }


}
