using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Range(0, 1)]
    public float followSpeed;
    [Range(0, 10)]
    public float movementSpeed;
    public const float camZ = -15;

    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        
        if (player != null)
        {
            Vector2 playerPos = player.position;
            Vector2 cameraPos = transform.position;


            //Interpolating between the initial camera positiona and the player position in 2D
            Vector2 lerpVal = Vector2.Lerp(cameraPos, playerPos, followSpeed);

            transform.position = new Vector3(
                lerpVal.x,
                Mathf.Max(lerpVal.y, cameraPos.y + movementSpeed/100),
                camZ
                );
        }
    }
}
