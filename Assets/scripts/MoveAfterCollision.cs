using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAfterCollision : MonoBehaviour
{
    bool hasBeenDefeated = false;
    Rigidbody parentRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        parentRigidbody = transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBeenDefeated)
            parentRigidbody.AddRelativeForce(new Vector3(0, -10000, 0));
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player") || collision.gameObject.name.Equals("back") || collision.gameObject.tag.Equals("EnemyShooter"))
        {
            Debug.Log("Enemy has been defeated");
            hasBeenDefeated = true;
        }
    }
    
}
