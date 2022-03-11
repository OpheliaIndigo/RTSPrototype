using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float maxHealth = 42f;
    public float currentHealth;
    
    public int barWidth = 50;
    public int barHeight = 10;
    float barYOffset;

    public float deselectedBarTransparency = 0.4f;
    public float selectedBarTransparency = 0.2f;

    Color actualColor;
    Color trueColor;

    public float testHPIncrement = 5;
    // Start is called before the first frame update
    void Start()
    {
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
        Selectable sel = GetComponent<Selectable>();

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

    private Rect getHealthRect(float heathPct)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 screenBotLeft = new Vector3(screenPos.x - barWidth / 2, screenPos.y + barYOffset - barHeight / 2, screenPos.z);
        Vector3 screenTopRight = new Vector3(screenPos.x - (barWidth / 2) + barWidth * heathPct, screenPos.y + barYOffset + barHeight / 2, screenPos.z);

        Rect rect = Utils.GetScreenRect(screenBotLeft, screenTopRight);

        return rect;
    }

    private void OnGUI()
    {
        Utils.DrawScreenRect(getHealthRect(currentHealth/maxHealth), getHealthColor());

    }

}
