using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float health;
    public float defense; 
    public float strength;
    public float shootCooldown;
    public float projectileStrength;
    public float projectileSpeed;

    private float timeShotted;
    
    public Rigidbody2D rb;
    public GameObject target;
    public GameObject projectile;
    public Transform tr;
    private Transform targetPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = target.GetComponent<Transform>();
        tr = GetComponent<Transform>();
        timeShotted = Time.time;
    }
    
    // Update is called once per frame
    void Update()
    {
        runAway();
    }

    void runAway() {
        Vector3 moveDir = tr.position - targetPos.position;
        moveDir = new Vector3(moveDir.x, moveDir.y, 0);
        transform.Translate(moveDir.normalized * speed * Time.deltaTime);
    }

    void FixedUpdate() {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        trackTarget();
        if(Input.GetMouseButton(0))
            shoot();
    }

    // collects all collisions
    void OnTriggerEnter2D(Collider2D collider) {
        // detect if hit by a projectile
        if(collider.gameObject.tag == "Projectile" && collider.gameObject.GetComponent<ProjectileBehavior>().parTag != "Enemy")
        {
            float projStrength = collider.gameObject.GetComponent<ProjectileBehavior>().strength;
            health -= Constants.calcDamage(projStrength, defense);
        }

        // detect if hit by melee attack
        if(collider.gameObject.tag == "Player")
        {
            float pStren = collider.gameObject.GetComponent<PlayerController>().strength;
            health -= Constants.calcDamage(pStren, defense);
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

    // shoot at the target
    void shoot()
    {
        if(Time.time - timeShotted > shootCooldown) // it's cooled down
        {
            Vector3 targPos = targetPos.position;

            GameObject boolet = GameObject.Instantiate(projectile, tr);
            boolet.GetComponent<ProjectileBehavior>().setup(projectileStrength, projectileSpeed, 
            new Vector2(targPos.x, targPos.y), tr, gameObject.tag);

            timeShotted = Time.time;
        }
    }
}
