using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using TMPro;
using System;

public class PenguinArea : Area
{
    public PenguinAgent penguinAgent;
    public PenguinAgent penguinAgent2;
    public FishAgent fishAgent;
    public GameObject penguinBaby;
    public Fish fishPrefab;
    public TextMeshPro cumulativeRewardTextp;
    public TextMeshPro cumulativeRewardTextf;

    [HideInInspector]
    public float fishAmount = 4f;

    [HideInInspector]
    public float fishSpeed = 0f;
    [HideInInspector]
    public float feedRadius = 1f;

    [HideInInspector]
    public List<GameObject> fishList;

    public override void ResetArea()
    {
        RemoveAllFish();
        PlacePenguin();
        PlaceBaby();
        SpawnFish(fishAmount, fishSpeed);

    }
    
    public void RemoveSpecificFish(GameObject fishObject)
    {
        fishList.Remove(fishObject);
        Destroy(fishObject);
    }
    
    public static Vector3 ChooseRandomPosition(Vector3 center, float minAngle, float maxAngle, float minRadius, float maxRadius)
    {
        float radius = minRadius;

        if (maxRadius >minRadius)
        {
            radius = UnityEngine.Random.Range(minRadius, maxRadius);

        }
        return center + Quaternion.Euler(0f, UnityEngine.Random.Range(minAngle, maxAngle), 0f) * Vector3.forward * radius;
    }

    private void RemoveAllFish()
    {
        if (fishList != null)
        {
            for (int i = 0; i< fishList.Count; i++)
            {
                if (fishList[i] != null)
                {
                    Destroy(fishList[i]);
                }
            }
        }
        fishList = new List<GameObject>();
    }

    private void PlacePenguin()
    {
        penguinAgent.transform.position = ChooseRandomPosition(transform.position, 0f, 360f, 0f, 9f) + Vector3.up * .5f;
        penguinAgent.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);

        penguinAgent2.transform.position = ChooseRandomPosition(transform.position, 0f, 360f, 0f, 9f) + Vector3.up * .5f;
        penguinAgent2.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);

    }

    private void PlaceBaby()
    {
        penguinBaby.transform.position = ChooseRandomPosition(transform.position, -45f, 45f, 4f, 9f) + Vector3.up * .5f;
        penguinBaby.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 180f), 0f);
    }

    private void SpawnFish(float count, float fishSpeed)
    {
        int countint = (int)Math.Round(count);
        for (int i = 0; i< countint; i++)
        {
            GameObject fishObject = Instantiate<GameObject>(fishPrefab.gameObject);
            fishObject.transform.position = ChooseRandomPosition(transform.position, 100f, 260, 2f, 13f) + Vector3.up * .5f;
            fishObject.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
            fishObject.transform.parent = transform;
            fishList.Add(fishObject);
            fishObject.GetComponent<Fish>().fishSpeed = fishSpeed;

        }
    }
    private void Update()
    {
        cumulativeRewardTextp.text = penguinAgent.GetCumulativeReward().ToString();
        cumulativeRewardTextf.text = fishAgent.GetCumulativeReward().ToString();

    }
}
