using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float clickStrenght=1;
    public bool activateGravity;

    Rigidbody m_Rigidbody;
    private Vector2 dir = new Vector2(0,0);
    private bool shouldUpdatePhyics;


    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    // Input need to be here insted of FixedUpdate otherwise we might lose inputs
    void Update()
    {
        //Screenspace is defined in pixels.
        //The bottom-left of the screen is (0, 0);
        //the right-top is (pixelWidth, pixelHeight).
        //The z position is in world units from the camera.
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.y < 0)
            GameManager.EndGame("slime went to bottom of the screen");

        //Todo add touch input
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                //Debug.Log("Touch pos: "+Input.GetTouch(i).position);
                
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                /*
                // Create a particle if hit
                if (Physics.Raycast(ray))
                {
                    Instantiate(particle, transform.position, transform.rotation);
                }*/
                
            }
        }
        if (Input.GetButton("Fire1"))
        {

            //Debug.Log("Screen mouse pos:"+Input.mousePosition);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           
            Vector2 dist = mousePos - transform.position;

            //Debug.Log("Distance is"+dist);

            setMovement(Vector3.Normalize(dist));
        }
        if (Input.GetKeyDown("space"))
        {
            activateGravity = true;
        }
        if (Input.GetKeyUp("space"))
        {
            activateGravity = false;
        }
    }

    //Sets the next movement that will be performed on the next cycle of the physic engine
    public void setMovement(Vector2 v2)
    {
        dir = v2;
        shouldUpdatePhyics = true;
    }

    //Used to update physics. The physics engine gets updated here
    private void FixedUpdate()
    {
        if (shouldUpdatePhyics)
        {
            move();
            shouldUpdatePhyics = false;
        }
        transform.position -= new Vector3(0,0,transform.position.z);
        //m_Rigidbody.AddForce(new Vector3(0, 0, -100* transform.position.z));
    }

    void move()
    {
        m_Rigidbody.AddForce(dir * clickStrenght);//Fixed update is fixed so you don't need delta time
    }


}
