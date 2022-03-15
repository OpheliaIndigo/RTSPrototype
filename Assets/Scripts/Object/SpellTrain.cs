﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTrain : Spell
{
    public GameObject unit;
    public int trainSeconds;

    public float progress;


    private bool isTraining = false;

    private int elapsedSeconds;
    private float currentTime;
    private Castable sender;

    public override void Cast(Castable sender)
    {
        if (!isTraining)
        {
            progress = 0f;
            elapsedSeconds = 0;
            currentTime = Time.time;
            isTraining = true;
            this.sender = sender;
        }
    }

    public void Start()
    {
        elapsedSeconds = 0;
        progress = 0f;
    }

    public void Update()
    {
        //currentTime = Time.time;
        if (Time.time - currentTime >= 1 && isTraining)
        {
            UpdateSecond();
            currentTime = Time.time;
        }
    }

    public void UpdateSecond()
    {
        elapsedSeconds++;
        progress = elapsedSeconds / (float)trainSeconds;
        if (progress >= 1)
        {
            Train();
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