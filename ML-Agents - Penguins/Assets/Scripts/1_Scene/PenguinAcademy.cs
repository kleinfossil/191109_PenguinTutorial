using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PenguinAcademy : Academy
{
    private PenguinArea[] penguinAreas;
    public float overwriteFishAmount;

    public override void AcademyReset()
    {
        //Get the penguin areas
        if (penguinAreas == null)
        {
            penguinAreas = FindObjectsOfType<PenguinArea>();
        
        }

        //Set up areas
        foreach (PenguinArea penguinArea in penguinAreas)
        {
            penguinArea.fishSpeed = resetParameters["fish_speed"];
            penguinArea.feedRadius = resetParameters["feed_radius"];
            penguinArea.fishAmount = resetParameters["fish_amount"];
            if (overwriteFishAmount != 4) { penguinArea.fishAmount = overwriteFishAmount; }
            
            penguinArea.ResetArea();
        }
    }
}
