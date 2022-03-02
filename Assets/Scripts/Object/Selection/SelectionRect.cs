using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionRect 
{
    public Vector3 start;
    public Vector3 end;

    LayerMask m_LayerMask;



    public SelectionRect(Vector3 start, Vector3 end, GameObject UnitController, LayerMask layerMask)
    {
        this.start = start;
        this.end = end;
        LineRenderer line = UnitController.GetComponent<LineRenderer>();
        line.positionCount = 5;
        line.useWorldSpace = true;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.loop = true;
        CreatePoints(line, start, end);
        QueryIntersection();
        m_LayerMask = layerMask;
    }

    void CreatePoints(LineRenderer lineRenderer, Vector3 start, Vector3 end) {

        float ypos = Mathf.Max(start.y, end.y) + 1;

        lineRenderer.SetPosition(0, new Vector3(start.x, start.y+1, start.z));
        lineRenderer.SetPosition(1, new Vector3(end.x, ypos, start.z));
        lineRenderer.SetPosition(2, new Vector3(end.x, end.y+1, end.z));
        lineRenderer.SetPosition(3, new Vector3(start.x, ypos, end.z));
        lineRenderer.SetPosition(4, new Vector3(start.x, start.y+1, start.z));
    }

    public GameObject[] QueryIntersection()
    {
        float dimX = Mathf.Abs(end.x - start.x);
        float dimZ = Mathf.Abs(end.z - start.z);
        MonoBehaviour.print(start.x + dimX / 2);
        MonoBehaviour.print(start.z + dimZ / 2);
        MonoBehaviour.print(end.x + dimX / 2);
        MonoBehaviour.print(end.z + dimZ / 2);

        MeshCollider selectionBox;

        Collider[] result = Physics.OverlapBox(
            new Vector3(
                    start.x + dimX / 2,
                    25,
                    start.z + dimZ / 2
                ),
            new Vector3(
                    dimX / 2,
                    150,
                    dimZ / 2
                ),
            Quaternion.identity, m_LayerMask
        );
        List<GameObject> gameObjects = new List<GameObject>();
        Selectable sel;
        foreach(Collider c in result)
        {
            MonoBehaviour.print(c.name);
            if (c.gameObject.TryGetComponent<Selectable>(out sel))
            {
                sel.Select();
                MonoBehaviour.print("Selection selected");
            }
        }

        return gameObjects.ToArray();
    }
}
