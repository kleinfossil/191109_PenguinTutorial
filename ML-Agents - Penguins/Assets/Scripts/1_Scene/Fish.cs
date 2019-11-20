using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class Fish : MonoBehaviour
{
    public float fishSpeed;

    private PenguinArea penguinArea;

    [HideInInspector]
    public RayPerception3D rayPerception;

    //private float randomizedSpeed = 0f;
    //private float nextActionTime = -1f;
    private Vector3 targetPosition;

    public float currentX = 0f;

    private void FixedUpdate()
    {
        /*if (fishSpeed > 0f)
        {
            Swim();
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("penguin"))
        {
            penguinArea.fishAgent.AddReward(-1f);
        }

    }

    private void Start()
    {
        penguinArea = GetComponentInParent<PenguinArea>();
    }

    /*private void Swim()
    {
        if (Time.fixedTime >= nextActionTime)
        {
            //Randomize the speed
            randomizedSpeed = fishSpeed * UnityEngine.Random.Range(.5f, 1.5f);

            //Pick a random position
            targetPosition = PenguinArea.ChooseRandomPosition(transform.parent.position, 100f, 260f, 2f, 13f);

            //Rotate toward the target
            transform.rotation = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);

            //Calculate the time to get there
            float timeToGetThere = Vector3.Distance(transform.position, targetPosition) / randomizedSpeed;
            nextActionTime = Time.fixedTime + timeToGetThere;
        }
        else
        {
            //Make sure that the fish does not swim past the target
            Vector3 moveVector = randomizedSpeed * transform.forward * Time.fixedDeltaTime;
            if (moveVector.magnitude <= Vector3.Distance(transform.position, targetPosition))
            {
                transform.position += moveVector;
            }
            else
            {
                transform.position = targetPosition;
                nextActionTime = Time.fixedTime;
            }
        }
    }*/
}
