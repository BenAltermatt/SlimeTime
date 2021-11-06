using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Stats associated with the player
    public float speed;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        move();
    }

    private void move() 
    {
        int x = (int) Input.GetAxisRaw("Horizontal");
        int y = (int) Input.GetAxisRaw("Vertical");

        float magnitude = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(x, 2));

        rb.velocity = new Vector2(x * speed / magnitude, y * speed / magnitude);
    }
}
