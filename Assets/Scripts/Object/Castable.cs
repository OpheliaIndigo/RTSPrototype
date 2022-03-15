using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castable : MonoBehaviour
{
    public List<Spell> spells;

    Selectable sel;

    // Start is called before the first frame update
    void Start()
    {
        sel = GetComponent<Selectable>();
        if (sel == null)
            enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (sel.selected)
        {
            for (int i = 0; i < HotkeyHandler.gridHotkeys.Length; i++)
            {
                if (Input.GetKeyDown(HotkeyHandler.gridHotkeys[i]))
                {
                    print("Cast spell " + HotkeyHandler.gridHotkeys[i].ToString());
                    Cast(i);
                }
            }
        }
    }

    void Cast(int i)
    {
        if (i < spells.Count)
        {
            spells[i].Cast(this);
        }
    }
}
