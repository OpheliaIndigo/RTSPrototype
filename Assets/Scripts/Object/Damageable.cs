using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float maxHealth = 42f;
    public float currentHealth;
    
    float barYOffset;

    public float deselectedBarTransparency = 0.4f;
    public float selectedBarTransparency = 0.1f;

    Color actualColor;
    Color trueColor;

    public float testHPIncrement = 5;

    Selectable sel;
    // Start is called before the first frame update
    void Start()
    {
        sel = GetComponent<Selectable>();
        sel.addBar();

        currentHealth = maxHealth;

        Rect objectScreenRect = Utils.BoundsToScreenRect(GetComponent<MeshFilter>().sharedMesh.bounds);
        barYOffset = objectScreenRect.height*2;
        print("setting barYOffset to " + barYOffset.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 0f)
        {
            currentHealth = 0f;
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        TestHealthLoseGain();
        sel.bars[0].setPctRect(currentHealth / maxHealth);
        sel.bars[0].setBarColor(getHealthColor());
    }

    void TestHealthLoseGain()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
            currentHealth -= testHPIncrement;
        else if (Input.GetKeyDown(KeyCode.KeypadPlus))
            currentHealth += testHPIncrement;

    }

    private Color getHealthColor()
    {
        actualColor = Color.Lerp(HotkeyHandler.minHealth, HotkeyHandler.maxHealth, currentHealth/maxHealth);
        trueColor = actualColor;
        sel = GetComponent<Selectable>();

        if (sel != null)
        {
            if (!sel.selected)
            {
                trueColor = Color.Lerp(actualColor, Color.clear, deselectedBarTransparency);
            }
            else
            {
                trueColor = Color.Lerp(actualColor, Color.clear, selectedBarTransparency);
            }
        }
        return trueColor;

    }


}
