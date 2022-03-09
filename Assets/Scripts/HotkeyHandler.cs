using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HotkeyHandler
{
    public static KeyCode addRemoveSelectionKey = KeyCode.LeftShift;
    public static int mouseButtonMove = 1;
    public static int mouseButtonSelect = 0;


    public static KeyCode[] gridHotkeys =
    {
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R,
        KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F,
        KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V
    };

    public static Color minHealth = Color.red;
    public static Color maxHealth = Color.green;
}
