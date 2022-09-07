using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate_Reward : MonoBehaviour
{
    private Transform rewardgeneratepos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GenerateReward();
    }

    void GenerateReward() {

        if (transform.childCount == 1) {
            rewardgeneratepos = this.transform.Find("RewardSpawn");
            GameObject reward = GameObject.CreatePrimitive(PrimitiveType.Cube);
            reward.transform.localScale = new Vector3(2, 2, 2);
            reward.transform.position = rewardgeneratepos.position;
            
            enabled = false;
            
        }
    }
}