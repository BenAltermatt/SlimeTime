using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Stats associated with the player
    public float speed;
    public float projectileSpeed;
    public float projectileStrength;

    // takes care of shooting projectiles
    public float projectileCooldown;
    private float timeShot;

    public Rigidbody2D rb;
    public GameObject projectile;
    public Transform tr;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        cam = Camera.main;
        timeShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        move();
        if(Input.GetMouseButton(0))
        {
            fire();
        }
    }

    private void move() 
    {
        int x = (int) Input.GetAxisRaw("Horizontal");
        int y = (int) Input.GetAxisRaw("Vertical");
        float magnitude = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));

        rb.velocity = new Vector2(x * speed, y * speed);

        if(magnitude != 0)
            rb.velocity = rb.velocity / magnitude;
    }

    private void fire() {

        if(Time.time - timeShot > projectileCooldown) 
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = cam.ScreenToWorldPoint(mousePos);

            GameObject shot = Instantiate(projectile, tr);
            shot.GetComponent<ProjectileBehavior>().setup(projectileStrength, projectileSpeed, new Vector2(mousePos.x, mousePos.y), tr, gameObject.tag);
            timeShot = Time.time;
        }
    }
}
