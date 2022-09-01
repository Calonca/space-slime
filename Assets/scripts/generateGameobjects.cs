using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class generateGameobjects : MonoBehaviour
{
    public GameObject[] prefab_celestials;
    public GameObject chunck;
    public float chuncksize = 32;
    bool visited = false;
    // Start is called before the first frame update

    void Start()
    {
        spawnTerrain();
    }
    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag.Equals("Player") && !visited)
        {
            //Debug.Log("Generating nearby chunks for chuck at position " + transform.position);
            spawnChunks();
            visited = true;
        }
    }
    
    void spawnChunks()
    {
        Vector3 topPosition = transform.position + new Vector3(0, chuncksize, 0);
        Vector3 bottomPosition = transform.position + new Vector3(0, -chuncksize, 0);
        Vector3 rigthPosition = transform.position + new Vector3(chuncksize, 0, 0);
        Vector3 leftPosition = transform.position + new Vector3(-chuncksize, 0, 0);
        
        Vector3 topRight = transform.position + new Vector3(chuncksize, chuncksize, 0);
        Vector3 topLeft = transform.position + new Vector3(-chuncksize, chuncksize, 0);
        Vector3 bottomRight = transform.position + new Vector3(chuncksize, -chuncksize, 0);
        Vector3 bottomLeft = transform.position + new Vector3(-chuncksize, -chuncksize, 0);
        Vector3[] nearbyChuckPositions = { topPosition, bottomPosition, rigthPosition, leftPosition, topRight, topLeft,bottomRight, bottomLeft};

        float halfSize = (chuncksize +1) / 2;
        //Create an array of nearby chucks
        Vector3[] collidedPos =
            Physics.OverlapBox(transform.position, new Vector3(halfSize,halfSize,halfSize))
            .Select(c => c.gameObject)
            .Where(o => o.tag.Equals("chuck"))
            //.Where(o => o.gameObject.GetComponent<generateGameobjects>().generated==false)
            .Select(c => c.transform.position)
            .Where(o => o != transform.position)
            .Where(pos => nearbyChuckPositions.Contains(pos))
            .ToArray();

        Vector3[] spawnPoss = nearbyChuckPositions.Except(collidedPos).ToArray();
        //print collidedGameObjects
        foreach (Vector3 spawnPos in spawnPoss)
        {
                //Debug.Log("Will spawn chuck at"+ spawnPos);
                GameObject newChunk = Instantiate(chunck, spawnPos, Quaternion.identity); //generate nearby missing chunks
                newChunk.transform.parent = transform.parent;
                newChunk.gameObject.name = "chuck" + spawnPos.ToString();
                //Instance is creating a copy so you need to delete the children on the old object
                foreach (Transform child in newChunk.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                //newChunk.GetComponent<generateGameobjects>().spawnTerrain();
        }
    }

    void spawnTerrain()
    {
        spawnobj_start();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").transform.position.y - chuncksize*2 >= transform.position.y)
        {
            Destroy(gameObject);
        }
    }

    void spawnobj_start(){    //random generation planets of 3 diff sizes  initially
        int x = Random.Range(20, 25); // number of objects to be generated initially
        float initial_pos_y, initial_pos_x; // Z axis is fixed, defining other two coordinates, i.e, X and Y.
        int i = 0; // variable for counter to generate objects

        
        while (i<x)
        {
            float halfSize = chuncksize / 2;
            initial_pos_x = Random.Range(-halfSize, halfSize) +transform.position.x; //range of X Coordinate to generate the planet object
            initial_pos_y = Random.Range(-halfSize, halfSize) +transform.position.y; //range of Y Coordinate to generate the planet object.Initially generated till 30 units on Y axis.

            
            /*x = 1;
            initial_pos_x = transform.position.x; //range of X Coordinate to generate the planet object
            initial_pos_y = transform.position.y; //range of Y Coordinate to generate the planet object.Initially generated till 30 units on Y axis.
            */

            bool isColliding = Physics.OverlapSphere(new Vector3(initial_pos_x, initial_pos_y, transform.position.z),2)
                .Where(c => !c.gameObject.tag.Equals("chuck"))
                .ToArray().Length > 0;

            if (!isColliding) // to check if any other sphere object(planet object ) generated within given pos and and radius
            {

                GameObject terrainObj = Instantiate(prefab_celestials[Random.Range(0, prefab_celestials.Length)], new Vector3(initial_pos_x, initial_pos_y, 0), Quaternion.identity); //generate the planet of any of 3 diff size on the X and Y pos if above confition is false
                terrainObj.transform.parent = transform;
                
            }
            i++;
        }

    }
    
}

