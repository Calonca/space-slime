using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float clickStrenght = 1;
    public float projectilesPerSecond = 3;
    public Transform firepoint;
    public GameObject bulletPrefab;
    Rigidbody rb; GameObject bullet; Vector3 direction; 
    Rigidbody m_Rigidbody;
    private Vector2 dir = new Vector2(0, 0);
    private Vector3 initialFingerToPlayerDist = new Vector2(0, 0);
    public static List<GameObject> Enemies = new List<GameObject>();
    private float closest_enemy_distance, distance;
    private bool joystickReleased = true;
    static float  turn;
    // Start is called before the first frame update

    bool isColliding = false;
    Vector3 cl;
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        InvokeRepeating("LaunchProjectile", 0.1f, 1/projectilesPerSecond);
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
            Vector3 mouse2d = new Vector3(mousePos.x, mousePos.y, 0);
            /*
            if (!isColliding)
            {
                m_Rigidbody.position = mouse2d;
            }
            else
            {
                //Transform hitObj = cl.transform;
                //Debug.Log(cl.gameObject.name);
                float distBefore = Vector3.Distance(cl, transform.position);
                float distAfter = Vector3.Distance(cl, mouse2d);

                //Debug.Log("Dis before" + distBefore);
                //Debug.Log("Dis after" + distAfter);
                if (distAfter>distBefore)
                {

                    m_Rigidbody.position = mouse2d;
                }
            }*/

            //isColliding=false;

            //Debug.Log("Distance is"+dist);
            if (joystickReleased)
            {
                //Saving distance from touch position to player position
                initialFingerToPlayerDist = transform.position - mouse2d;
                joystickReleased = false;
            }

            //setMovement(new Vector3(mousePos.x,mousePos.y,0));
            if (!joystickReleased)
            {
                Vector3 vel = (mouse2d + initialFingerToPlayerDist) - transform.position;
                m_Rigidbody.velocity = vel * 20;
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            joystickReleased = true;
        }

        aimenemyauto();
        //shoot();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Planet"))
        {

            cl = collision.gameObject.transform.position;
            isColliding = true;

            //Debug.Log(cl.gameObject.name);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Planet"))
        {
            isColliding = false;
        }
    }

    //Sets the next movement that will be performed on the next cycle of the physic engine
    public void setMovement(Vector2 v2)
    {
        dir = v2;
       
    }

    //Used to update physics. The physics engine gets updated here
    private void FixedUpdate()
    {

        Vector3 forwardDir = new Vector3(0, 1, 0);
        m_Rigidbody.AddForce(5 * forwardDir);//Move up
        move();

    }

    void move()
    {
        Vector3 v3 = dir * clickStrenght;
        //m_Rigidbody.position += v3;
        //m_Rigidbody.AddForce(500*dir * clickStrenght);//Fixed update is fixed so you don't need delta time

    }

    GameObject closestEnemy = null;
    void aimenemyauto()
     {
        closestEnemy = null;
        closest_enemy_distance = 10;
         foreach (GameObject en in GameObject.FindGameObjectsWithTag("EnemyShooter"))
         {
             distance = Vector3.Distance(en.transform.position, transform.position);
             if (distance < closest_enemy_distance)
             {
                closestEnemy = en;
                closest_enemy_distance = distance;
             }
         }
        if (closestEnemy == null)
            return;
        //transform.LookAt(closestEnemy.transform, new Vector3(0,1,0));
        Vector3 differences = (closestEnemy.transform.position - transform.position).normalized;
        Debug.Log("Angle is " + Mathf.Atan2(differences.y, differences.x));
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(differences.y, differences.x));
        //Vector3 forward = direction - up * Vector3.Dot(direction, up);
        //transform.rotation = Quaternion.LookRotation(forward.normalized, up.normalized);
        //Debug.Log("Closest enemy at position " + closestEnemy.transform.position);
        closest_enemy_distance = distance;
    }

    void LaunchProjectile()
    {
       if (!joystickReleased || closestEnemy==null) { 
            return; }
        bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        rb = bullet.GetComponent<Rigidbody>();

        rb.AddForce((closestEnemy.transform.position-transform.position).normalized * 150f, ForceMode.Impulse);
        // rb.AddForce(transform.LookAt(GameObject.Find("Player").transform) * bulletForce * 15f, ForceMode.Impulse);
        Destroy(bullet, 2f);
        // Debug.Log("fire");
    }


    void shoot() {
        if (Input.GetKey("q"))
        {
            turn = turn + Time.deltaTime;
            transform.rotation = Quaternion.Euler(0,0, turn * 100f);
        }

        if (Input.GetKey("e"))
        {
            turn = turn - Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, turn * 100f);
        }

         if (Input.GetButtonDown("Fire2")) {


            LaunchProjectile();
        }
    }
}
