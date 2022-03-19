using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectBar : MonoBehaviour {
    public int barWidth;
    public int barHeight;
    public float barIndex;

    private float barYBasePosition;

    private Rect pctRect;
    private Color barColor;

    public void Start() { 
        
    }


    public ObjectBar(int barWidth, int barHeight, int barIndex)
    {
        this.barWidth = barWidth;
        this.barHeight = barHeight;
        this.barIndex = barIndex;
    }

    public void setPctRect(float barPct)
    {
        barYBasePosition = barIndex * barHeight;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 screenBotLeft = new Vector3(screenPos.x - barWidth / 2, screenPos.y + barYBasePosition - barHeight / 2, screenPos.z);
        Vector3 screenTopRight = new Vector3(screenPos.x - (barWidth / 2) + barWidth * barPct, screenPos.y + barYBasePosition + barHeight / 2, screenPos.z);

        Rect rect = Utils.GetScreenRect(screenBotLeft, screenTopRight);

        pctRect = rect;

    }

    public void setBarColor(Color barColor)
    {
        this.barColor = barColor;
    }

    private void OnGUI()
    {
        Utils.DrawScreenRect(pctRect,barColor);

    }




}
