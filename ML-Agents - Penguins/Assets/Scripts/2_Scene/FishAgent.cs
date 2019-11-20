using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System;

public class FishAgent : Agent
{


    private PenguinArea penguinArea;

    public PenguinAgent penguin1;
    public PenguinAgent penguin2;
    private RayPerception3D rayPerception;

    public Rigidbody rb;

    private Fish currentActiveFish;
    private int fishCount;

    private void Start()
    {
        penguinArea = GetComponentInParent<PenguinArea>();
    }

    public Fish SetActiveFish(int activeFish)
    {
        return currentActiveFish = penguinArea.fishList[activeFish].GetComponent<Fish>();
    }

    public override void CollectObservations()
    {

        float rayDistance = 2f;
        float[] rayAngles = { 30f, 60f, 90f, 120f, 150f, 180f, 210f, 240f, 270f, 300f, 330f, 360f };
        string[] detectableObjects = { "penguin","wall" };

        for (int fishCount = 0; fishCount<penguinArea.fishAmount;fishCount++)
        {
            if (penguinArea.fishList.Count > fishCount)
            {
                Fish activeFish = SetActiveFish(fishCount);
                rayPerception = activeFish.GetComponent<RayPerception3D>();
                //Provide the current active fish number
                AddVectorObs(fishCount);

            }else
            {
                rayPerception = penguinArea.fishAgent.GetComponent<RayPerception3D>();
                AddVectorObs(fishCount);
            }
            AddVectorObs(rayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
            
        }
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        //Convert actions to axis values

        float forward = vectorAction[0];
        int whichFish = (int)Math.Round(vectorAction[2]);

        if (whichFish<=penguinArea.fishList.Count &&whichFish >0)
        {

            AddReward(0.0001f);
            Fish activeFish = SetActiveFish(whichFish-1);


            rb = activeFish.GetComponent<Rigidbody>();
            if (vectorAction[1] == 1f)
            {

                activeFish.currentX = activeFish.currentX + 1f;

            }
            else if (vectorAction[1] == 2f)
            {

                activeFish.currentX = activeFish.currentX - 1f;
            }

            Quaternion delateRotation = Quaternion.Euler(0f, activeFish.currentX, 0f);
            rb.MoveRotation(delateRotation);

            forward = forward * 100;
            rb.AddForce(activeFish.transform.forward * forward);
           
        }


        //Tiny reward every step
        if (penguinArea.fishList.Count > 0)
        {
            AddReward((0.01f * penguinArea.fishList.Count) / agentParameters.maxStep);
        }


    }



    public override float[] Heuristic()
    {

       
        float[] playerInput = { 0f, 0f, 0f };
        float output = 0f;
      
        if (Input.GetKey(KeyCode.Z) && float.TryParse(Input.inputString, out output) )
        {
            playerInput[0] = 1;
            playerInput[2] = output;
        }
        if (Input.GetKey(KeyCode.G) && float.TryParse(Input.inputString, out output))
        {
            playerInput[1] = 1;
            playerInput[2] = output;
        }
        if (Input.GetKey(KeyCode.J) && float.TryParse(Input.inputString, out output))
        {
            playerInput[1] = 2;
            playerInput[2] = output;
        }
        
       

        return playerInput;
    }

    



    public override void AgentReset()
    {
        penguinArea.ResetArea();

    }
}

