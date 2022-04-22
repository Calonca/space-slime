using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float clickStrenght=1;
    Rigidbody m_Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
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

            m_Rigidbody.AddForce(Vector3.Normalize(dist)*clickStrenght*Time.deltaTime);
        }
    }


}
