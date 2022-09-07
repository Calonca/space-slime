using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;
    public float bulletForce = 999999999f;
     public float dist;
     float cooldown=0.75f;
    private float cooldownTimer;
    Rigidbody rb; GameObject bullet; Vector3 direction;
    private  int health,maxhealth; Transform healthbar;
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        maxhealth=3;
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

        bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        rb = bullet.GetComponent<Rigidbody>();
        direction = GameObject.Find("Player").transform.position - firepoint.position;

        rb.AddForce(direction.normalized * bulletForce*15f,ForceMode.Impulse);
       // rb.AddForce(transform.LookAt(GameObject.Find("Player").transform) * bulletForce * 15f, ForceMode.Impulse);
        Destroy(bullet, 2f);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Slime Bullet")
        {

            if (health == 1)
            {
                Destroy(gameObject);
            }
            else
            {
                --health;
                healthbarscale(health);

            }
            Destroy(col.gameObject);
        }
    }

    void healthbarscale(int health) {
        healthbar= this.transform.Find("Health");
        healthbar.localScale = new Vector3(healthbar.localScale.x, 
            (healthbar.localScale.y) *(float) health /(float) maxhealth,
            healthbar.localScale.z); 
    }
}
