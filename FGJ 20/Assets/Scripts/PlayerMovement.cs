using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float boatSpeed = 3f;
    public float plankSpeed = 2f;
    public float waterSpeed = 1f;
    public float sawSpeed = 1.5f;
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
    public PlayerAudioController pac;
    public HitCDBarHandler hitCD;
    public int tackleCDtime;
    public float tackleRadius;

    private bool hammertime = false;
    private int cooldown = 10;
    private int tackleCD;
    private int speedMultiplier = 3;
    private Collider2D[] results;
    private int[] wallDirection = {0, 0};
    private bool isSwimming = false;
    private PlayerAudioController.State sound = PlayerAudioController.State.stop;
    /*
    private enum Ground {boat, plank, water, running};
    private Ground ground;
    */

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tackleCD = tackleCDtime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sound = PlayerAudioController.State.stop;

        if (tackleCD != 0) {
            tackleCD--;
        }
        
        hitCD.SetBar((float) tackleCD / (float) tackleCDtime);

        wallDirection[1] = 0;
        wallDirection[0] = 0;

		if(psa.isComboing) {
            // Set hammering animation here
            sound = PlayerAudioController.State.stop;
            animator.SetBool("Sawtime", false);
            animator.SetBool("Hammertime", true);
            sound = PlayerAudioController.State.stop;
            pac.SetSoundState(sound);
            return;
        } 
        else 
        {
            animator.SetBool("Hammertime", false);

        }

        bool hitted = false;
        if(tackleCD <= 0 && Input.GetButton(hammerbutton)) {
            hitted = true;
            tackleCD = tackleCDtime;
            pac.PlaySwing();
        }

        results = Physics2D.OverlapCircleAll(transform.position, ownCollider.size.x*tackleRadius);
        if (results.Length > 0) 
        {
            for (int i=0; i<results.Length; i++) 
            {
                if(hitted) {
                    if (results[i].gameObject.tag == "Player" && results[i] != ownCollider) {
                        var enemy = results[i].gameObject;
                        
                        pac.PlayHit();
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
                            
                if (results[i].gameObject.name == "BorderN") {
                    wallDirection[0] = 1;
                    Debug.Log("Wall");
                } else if (results[i].gameObject.name == "BorderS") {
                    wallDirection[0] = 2;
                    Debug.Log("Wall");
                }
                if (results[i].gameObject.name == "BorderW") {
                    wallDirection[1] = 1;
                    //Debug.Log("Wall");
                } else if (results[i].gameObject.name == "BorderE") {
                    wallDirection[1] = 2;
                    //Debug.Log("Wall");
                }
            }
        }

        Move();
        
        GetDash();

        pac.SetSoundState(sound);

    }



    void GetDash()
    {
        if (Input.GetButton(dashbutton) && !psa.isSawing)
        {
            if (cooldown <= 0)
            {
                //Debug.Log("Dash");
                pac.PlayDash();
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
        else {
            sound = PlayerAudioController.State.running;
        }
    }

    void Move()
    {

        if (psa.isSawing) {
            speed = sawSpeed;
        }

        float v = Input.GetAxis(vertical);
        float h = Input.GetAxis(horizontal);

        float dy = v * speed * speedMultiplier * Time.deltaTime;
        float dx = h * speed * speedMultiplier * Time.deltaTime;

        if ((wallDirection[0] == 1 && dy > 0) || (wallDirection[0] == 2 && dy < 0)) {
            //Debug.Log("dy = 0");
            dy = 0;
        }
        if ((wallDirection[1] == 1 && dx > 0) || (wallDirection[1] == 2 && dx < 0)) {
            //Debug.Log("dx = 0");
            dx = 0;
        }

        if ((dy != 0) || (dx != 0))
        {
            // Sounds
            
            if(isSwimming) {
                sound = PlayerAudioController.State.swimming;
            } 
            else {
                sound = PlayerAudioController.State.walking;
            }

            rb.MoveRotation(Mathf.Atan2(v, h) * 180 / Mathf.PI + 90);

            if (psa.isCarrying) 
            {
                animator.SetBool("Walk", false);  
                animator.SetBool("PlankWalk", true);
            } else if (psa.isSawing) 
            {
                sound = PlayerAudioController.State.sawing;
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
            sound = PlayerAudioController.State.stop;
            if (psa.isCarrying) 
            {
                animator.SetBool("PlankWalk", false);
                animator.SetBool("Planktime", true);
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
            speed = boatSpeed;
            isSwimming = false;
        }
        else if (col.gameObject.tag == "Plank")
        {
            speed = plankSpeed;
            isSwimming = false;
        }
        else {
            isSwimming = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boat" || col.gameObject.tag == "Plank")
        {
            speed = waterSpeed;
            isSwimming = true;
        }
    }

}