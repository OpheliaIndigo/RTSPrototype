using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Selectable))]
public class SelectableUI : Editor
{
    // Start is called before the first frame update
    void OnSceneGUI()
    {
        Selectable selected = (Selectable)target;

        Handles.color = Color.red;
        if (selected.selected)
        {
            Vector3 pos = selected.transform.position;
            pos.y = 0;
            Handles.DrawWireDisc(pos, new Vector3(0, 1, 0), selected.baseRadius);
        }
    }
}
