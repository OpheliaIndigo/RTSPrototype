using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float maxHealth = 42f;
    public float currentHealth;
    public int barWidth = 100;
    public int barHeight = 10;

    public float testHPIncrement = 5;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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
        return Color.Lerp(HotkeyHandler.minHealth, HotkeyHandler.maxHealth, currentHealth/maxHealth);
    }

    private Rect getHealthRect(float heathPct)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 screenBotLeft = new Vector3(screenPos.x - barWidth / 2, screenPos.y - barHeight / 2, screenPos.z);
        Vector3 screenTopRight = new Vector3(screenPos.x - (barWidth / 2) + barWidth * heathPct, screenPos.y + barHeight / 2, screenPos.z);

        Rect rect = Utils.GetScreenRect(screenBotLeft, screenTopRight);
        return rect;
    }

    private void OnGUI()
    {
        Utils.DrawScreenRect(getHealthRect(currentHealth/maxHealth), getHealthColor());
    }

}
