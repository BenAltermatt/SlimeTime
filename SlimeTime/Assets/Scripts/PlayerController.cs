using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Math;

public class PlayerController : MonoBehaviour
{
    // Stats associated with the player
    public float speed;

    public RigidBody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<RigidBody2D>();
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
        int x = (int) Input.getAxisRaw("Horizontal");
        int y = (int) Input.getAxisRaw("Vertical");

        Vector2 dirVec = new Vector2(x, y);
        dirVec.Scale(speed / Math.sqrt(dirVec.sqrMagnitude));

        rb.velocity = dirVec;
    }
}
