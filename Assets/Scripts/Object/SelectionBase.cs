using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBase
{
    public static GameObject CreateSelectionBaseObject(GameObject basePrefab, GameObject parent)
    {
        GameObject selectionBaseInstance = GameObject.Instantiate(basePrefab);
        selectionBaseInstance.transform.SetParent(parent.transform);

        Vector3 basePosition = calculateBasePosition(parent);
        selectionBaseInstance.transform.position = basePosition;

        return selectionBaseInstance;
    }

    public static void UpdateSelectionBaseObject(GameObject selectionBase, GameObject parent)
    {
        selectionBase.transform.position = calculateBasePosition(parent);
    }

    private static Vector3 calculateBasePosition(GameObject parent)
    {

        GameObject terrain = TerrainHandler.GetTerrainObject();
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
