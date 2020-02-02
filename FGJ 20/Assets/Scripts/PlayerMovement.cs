using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public string horizontal;
    public string vertical;
    public string hammerbutton;
    public string dashbutton;
    public int dashtime;
    public Animator animator;
    public PlayerSawingAction psa;
    public Rigidbody2D rb;
    public BoxCollider2D ownCollider;
    public PlayerHammerAction pha;

    private bool hammertime = false;
    private int cooldown = 10;
    private int speedMultiplier = 3;
    private Collider2D[] results;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(psa.isComboing) {
            // Set hammering animation here
            animator.SetBool("Sawtime", false);
            animator.SetBool("Hammertime", true);
            return;
        } 
        else 
        {
            animator.SetBool("Hammertime", false);

        }
        results = Physics2D.OverlapCircleAll(transform.position, ownCollider.size.x/2);
        if (results.Length > 0) 
        {
            for (int i=0; i<results.Length; i++) 
            {
                if (results[i].gameObject.tag == "Player" && results[i] != ownCollider) {
                    var enemy = results[i].gameObject;
                    if (Input.GetButton(hammerbutton)) 
                    {
                        Debug.Log("It's hammer time");
                        // Get direction
                        Vector3 direction;
                        var myPos = transform.position;
                        var enemyPos = enemy.transform.position;
                        var heading = enemyPos-myPos;

                        direction = heading.normalized;
                        // Start hammer action
                        enemy.GetComponent<PlayerHammerAction>().initFly(direction);
                    }   
                }
            }
        }

        Move(speed);
        
        GetDash();

    }



    void GetDash()
    {
        if (Input.GetButton(dashbutton))
        {
            if (cooldown == 0)
            {
                Debug.Log("Dash");
                cooldown = 120;
                speedMultiplier = 3;
            }
        }
        if (cooldown != 0)
        {
            cooldown--;
        }
        if (cooldown < 120 - dashtime)
        {
            speedMultiplier = 1;
        }
    }

    void Move(float speed)
    {
        float v = Input.GetAxis(vertical);
        float h = Input.GetAxis(horizontal);

        float dy = v * speed * speedMultiplier * Time.deltaTime;
        float dx = h * speed * speedMultiplier * Time.deltaTime;

        if ((dy != 0) || (dx != 0))
        {
            rb.MoveRotation(Mathf.Atan2(v, h) * 180 / Mathf.PI + 90);

            if (psa.isCarrying) 
            {
                animator.SetBool("Walk", false);  
                animator.SetBool("PlankWalk", true);
            } else if (psa.isSawing) 
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Sawtime", true);
            } else 
            {
                animator.SetBool("PlankWalk", false);
                animator.SetBool("Walk", true);    
            }
            
            
        }
        else
        {
            if (psa.isCarrying) 
            {
                animator.SetBool("PlankWalk", false);
                animator.SetBool("Plank", true);
            } else if (psa.isSawing) 
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Sawtime", true);
            } else 
            {
                animator.SetBool("Walk", false);    
            }
        }
        Vector2 newPosition = new Vector2(transform.position.x + dx, transform.position.y + dy);

        transform.position = newPosition;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boat")
        {
            speed = 3;
        }
        else if (col.gameObject.tag == "Plank")
        {
            speed = 2;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boat" || col.gameObject.tag == "Plank")
        {
            speed = 1;
        }
    }

}