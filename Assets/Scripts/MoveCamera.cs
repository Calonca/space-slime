using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float cameraSpeed;
    public const float camZ = -15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        if (player != null)
        {
            Vector2 playerPos = player.position;
            Vector2 cameraPos = transform.position;


            //Interpolating between the initial camera positiona and the player position in 2D
            Vector2 lerpVal = Vector2.Lerp(cameraPos, playerPos, cameraSpeed * Time.deltaTime);

            transform.position = new Vector3(lerpVal.x,lerpVal.y,camZ);
        }
    }
}
