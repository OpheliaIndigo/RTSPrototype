﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public int segments;
    public float xradius;
    public float zradius;
    public float yposition;
    public float baseThickness = 0.2f;
    LineRenderer line;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.gameObject.layer = LayerMask.NameToLayer("UI");

        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        line.startWidth = baseThickness;
        line.endWidth = baseThickness;
        CreatePoints();
        gameObject.SetActive(false);
    }


    void CreatePoints()
    {
        float x;
        float y = yposition;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * zradius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }
}
