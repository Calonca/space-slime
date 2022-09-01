using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;
    public float bulletForce = 400f;
     public float dist;
     float cooldown=0.75f;
    private float cooldownTimer;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
         dist = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position);
       
        if (dist < 15.0f )
        {
             Shoot();


        }

    }

    public void Shoot() {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer > 0) return;

        cooldownTimer = cooldown;

        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Vector3 direction = GameObject.Find("Player").transform.position - firepoint.position;
        rb.AddForce(direction* bulletForce,ForceMode.Impulse);
        
        Destroy(bullet, 2f);
    }
}
