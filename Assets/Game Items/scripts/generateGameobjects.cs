using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateGameobjects : MonoBehaviour
{
    static int p=25;//variable used to track position generate object
    public GameObject[] prefab_celestials;
    // Start is called before the first frame update

    void Start()
    {
   
        spawnobj_start();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").transform.position.y >= p)
        {
            spawnobj(); //create planet objects
            destroy_obj();// destroy used objects
        }
        //  Instantiate(prefab_celestials, new Vector3(0, 90, 0), Quaternion.identity);
    }

    void spawnobj_start(){    //random generation planets of 3 diff sizes  initially
        int x = (int)Random.Range(20, 25); // number of objects to be generated initially
        float initial_pos_y, initial_pos_x; // Z axis is fixed, defining other two coordinates, i.e, X and Y.
        int i = 0; // variable for counter to generate objects

        
        while (i<x)
        {
            initial_pos_x = Random.Range(-7, 7); //range of X Coordinate to generate the planet object
            initial_pos_y = Random.Range(8, 30); //range of Y Coordinate to generate the planet object.Initially generated till 30 units on Y axis.

            if (!Physics.CheckSphere(new Vector3(initial_pos_x, initial_pos_y, 0), 2)) // to check if any other sphere object(planet object ) generated within given pos and and radius
            {

                Instantiate(prefab_celestials[Random.Range(0, prefab_celestials.Length)], new Vector3(initial_pos_x, initial_pos_y, 0), Quaternion.identity); //generate the planet of any of 3 diff size on the X and Y pos if above confition is false
                i++;
            }

         
        }

    

    }
    
    
 

    void spawnobj(){    //random generation planets of 3 diff sizes  initially
        int x = (int)Random.Range(20, 25); // number of objects to be generated
        float pos_y, pos_x; // Z axis is fixed, defining other two coordinates, i.e, X and Y.
        int i = 0; // variable for counter to generate objects


        while (i < x)
        {
            pos_x = Random.Range(-7,7); //range of X Coordinate to generate the planet object
            pos_y = Random.Range(p+5,p+35);  //range of Y Coordinate to generate the planet object
            /* sample value of p and range of Y within which objects are generated
            p    min max      
            25   30  60
            55   60  90
            85   90  120
            115  120 150
            .
            .
            .
            */
            Vector3 pos = new Vector3(pos_x, pos_y, 0);

            if (!Physics.CheckSphere(pos, 2)) // to check if any other sphere object(planet object ) generated within given pos and and radius
            {

                Instantiate(prefab_celestials[Random.Range(0, prefab_celestials.Length)], pos, Quaternion.identity); //generate the planet of any of 3 diff size on the X and Y pos if above confition is false
                i++;
            }


        }

        p+=30;

    }

    void destroy_obj() {
        GameObject[] planet = GameObject.FindGameObjectsWithTag("Planet");

        foreach (GameObject temp in planet) {

            if (temp.transform.position.y <p-45)
            {

                Destroy(temp); //destroys the planet objects which has y position  below the  value p-45
            }

        }


    }
}

