using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public Renderer renderer;

    public float strength;

    // Start is called before the first frame update
    void Start()
    {
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
        // if its not hitting itself or the player
        if(collision.gameObject.tag != "Projectile")
            Destroy(gameObject);
    }
}
