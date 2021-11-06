using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float health;
    public float defense; 
    
    public Rigidbody2D rb;
    public GameObject target;
    private Transform targetPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = target.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        trackTarget();
    }

    // collects all collisions
    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Hit!");
        // detect if hit by a projectile
        if(collider.gameObject.tag == "Projectile")
        {
            float projStrength = collider.gameObject.GetComponent<ProjectileBehavior>().strength;
            health -= Constants.calcDamage(projStrength, defense);
        }
    }

    // properly follows the target
    void trackTarget()
    {
        Vector3 targPos = targetPos.position;
        Vector2 trajectory = new Vector2(targPos.x- rb.position.x, targPos.y - rb.position.y);
        float magnitude = trajectory.sqrMagnitude;

        if(magnitude != 0)
        {
            trajectory = trajectory * speed / magnitude;
            rb.velocity = trajectory;
        }

    }
}
