using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Stats associated with the player
    public float health;
    public float speed;
    public float defense;
    public float projectileSpeed;
    public float projectileStrength;
    public float strength;
    public float swingSpeed;

    // takes care of shooting projectiles
    public float projectileCooldown;
    private float timeShot;

    // takes care of melee attacks
    public float meleeCooldown;
    private float timeSwung;
    public int swingDir;

    public Rigidbody2D rb;
    public GameObject projectile;
    public Transform tr;
    public Camera cam;
    public Animator anim;

    // hitboxes and hurtboxes
    public Collider2D hurtBox;
    public Collider2D[] hitBoxes;

    public const int RIGHT = 0;
    public const int DOWN = 1;
    public const int LEFT = 2;
    public const int UP = 3;

    private int lastDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main;
        timeShot = Time.time;
        timeSwung = Time.time;

        lastDir = DOWN;
        swingDir = DOWN;
        health = 100;

        Collider2D[] boxes = GetComponents<Collider2D>();
        hitBoxes = new Collider2D[4];
        hurtBox = boxes[5];
        for(int i = 1; i < boxes.Length - 1; i++)
        {
            hitBoxes[i - 1] = boxes[i];
            hitBoxes[i - 1].enabled = false;
        }
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
        if(Time.time - timeSwung > swingSpeed)
        {
            hitBoxes[swingDir].enabled = false;
        }
        if(Input.GetKey("space"))
        {
            attack();
        }
    }

    private void move() 
    {
        int x = (int) Input.GetAxisRaw("Horizontal");
        int y = (int) Input.GetAxisRaw("Vertical");
        float magnitude = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));

        if(y!= 0)
        {
            lastDir = y + 2;
        }
        else if(x != 0)
        {
            lastDir = -1 * x + 1;
        }
        
        anim.SetInteger("Direction", lastDir);
        Debug.Log(anim.GetInteger("Direction"));
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

    public void attack()
    {
        if(Time.time - timeSwung > meleeCooldown)
        {
            hitBoxes[lastDir].enabled = true;
            swingDir = lastDir;
            timeSwung = Time.time;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.IsTouching(hurtBox))
        {   
            if(collider.gameObject.tag == "Projectile" && collider.gameObject.GetComponent<ProjectileBehavior>().parTag != "Player")
            {
                Debug.Log("hit.");
                float str = collider.gameObject.GetComponent<ProjectileBehavior>().strength;
                Debug.Log(str);
                health -= Constants.calcDamage(str, defense);
            }

            if(collider.gameObject.tag == "Enemy")
            {
                float str = collider.gameObject.GetComponent<EnemyController>().strength;
                health -= Constants.calcDamage(str, defense);
            }
        }

        
    }

}
