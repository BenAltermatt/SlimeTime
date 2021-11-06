using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public Renderer renderer;
    public Vector2 aim;
    public Transform tr;
    public Rigidbody2D rb;

    public float strength;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(!renderer.isVisible)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        // if its not hitting itself 
        if(collision.gameObject.tag != "Projectile")
            Destroy(gameObject);
    }

    public void setup(float strn, float spd, Vector2 aim, Transform ogPos)
    {
        tr = ogPos;
        this.aim = aim;
        strength = strn;
        speed = spd;
        rb = GetComponent<Rigidbody2D>();
        launch();
    }

    void launch()
    {
        Vector2 trajectory = new Vector2(aim.x - tr.position.x, aim.y - tr.position.y);
            
        float magnitude = Mathf.Sqrt(trajectory.sqrMagnitude);

        if(magnitude > 0)
        {
            trajectory = new Vector2(trajectory.x * speed / magnitude, trajectory.y  * speed/ magnitude);
        }

        rb.velocity = trajectory;
    }
}
