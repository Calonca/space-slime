using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("collision");
        if (collision.gameObject.tag.Equals("Player"))
        {
            GameManager.EndGame("slime was killed by enemy");
        }
    }
}
