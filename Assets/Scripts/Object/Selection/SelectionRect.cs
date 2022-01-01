using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionRect 
{
    public SelectionRect(Vector3 start, Vector3 end, GameObject UnitController)
    {

        LineRenderer line = UnitController.GetComponent<LineRenderer>();
        line.positionCount = 4;
        line.useWorldSpace = true;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;

        CreatePoints(line, start, end);
    }

    void CreatePoints(LineRenderer lineRenderer, Vector3 start, Vector3 end) {
        float xpos;
        float ypos;
        float zpos;
        for (int j = 0; j < 4; j++) {
            bool varyX = j % 2 == 0;

            if (varyX)
            {
                xpos = start.x + (end.x - start.x);
                zpos = j == 0 ? start.z : end.z;
            }
            else {
                xpos = j == 3 ? start.x : end.x;
                zpos = start.z + (end.z - start.z);
            }
            ypos = Terrain.activeTerrain.SampleHeight(new Vector3(xpos, 0, zpos)) + 1;

            lineRenderer.SetPosition(j, new Vector3(xpos,ypos,zpos));
        }


        //float yValue = terrain.GetComponent<Terrain>().activeTerrain.SampleHeight(new Vector3(xmin));
    }
}
