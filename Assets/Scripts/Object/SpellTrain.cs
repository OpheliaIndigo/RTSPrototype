using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTrain : Spell
{
    public GameObject unit;
    public int trainSeconds;

    public float progress;


    private bool isTraining = false;

    private float elapsedTime;
    private float castTime;
    private Castable sender;

    private int barIndex;

    public override void Cast(Castable sender)
    {
        if (!isTraining)
        {
            progress = 0f;
            elapsedTime = 0f;
            castTime = Time.time;
            isTraining = true;
            this.sender = sender;
            barIndex = sender.sel.addBar();
            sender.sel.bars[barIndex].setBarColor(HotkeyHandler.baseBar);


        }
    }

    public void Start()
    {
        elapsedTime = 0f;
        progress = 0f;
    }

    public void Update()
    {
        //currentTime = Time.time;
        if (isTraining)
        {
            UpdateBar();
        }
        
    }

    public void UpdateBar()
    {
        // Check 
        elapsedTime = Time.time - castTime;
        progress = elapsedTime / trainSeconds;
        sender.sel.bars[barIndex].setPctRect(progress);
        if (progress >= 1)
        {
            Train();
            sender.sel.removeBar(barIndex);
            isTraining = false;
        }
    }

    public void Train()
    {
        GameObject trained = Instantiate(unit);
        trained.transform.position = sender.transform.position;
        trained.transform.parent = sender.transform.parent;

        if (sender != null)
        {
            trained.GetComponent<Selectable>().moveToPosition(sender.GetComponent<Selectable>().rally);
        }
    }
}
