using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Stats associated with the player
    public float speed;
    public float projectileSpeed;

    public Rigidbody2D rb;
    public GameObject projectile;
    public Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
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
        Vector3 mousePos = Input.mousePosition;
        Vector2 trajectory = new Vector2(mousePos.x - rb.position.x, mousePos.y - rb.position.y);
        
        float magnitude = Mathf.Sqrt(trajectory.sqrMagnitude);

        if(magnitude > 0)
        {
            trajectory = new Vector2(trajectory.x / magnitude, trajectory.y / magnitude);
        }

        GameObject shot = Instantiate(projectile, tr);
        shot.GetComponent<Rigidbody2D>().velocity = trajectory;
    }
}
