﻿using System.Collections;
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
            hammertime = true;
            animator.SetBool("Hammertime", hammertime);
            return;
        } 
        else 
        {
            hammertime = false;
            animator.SetBool("Hammertime", hammertime);

        }
        results = Physics2D.OverlapCircleAll(transform.position, ownCollider.size.x/2);
        if (results.Length > 0) 
        {
            for (int i=0; i<results.Length; i++) 
            {
                if (results[i].gameObject.tag == "Player" && results[i] != ownCollider) {
                    var enemy = results[i].gameObject;
                    if (Input.GetButton(hammerbutton)) {
                        Debug.Log("It's hammer time");
                        enemy.GetComponent<PlayerSawingAction>().CancelCombo();
                        var myPos = transform.position;
                        var enemyPos = enemy.transform.position;
                        int moveX;
                        int moveY;
                        if (enemyPos.x - myPos.x > 0) {
                            moveX = 1;
                        } else {
                            moveX = -1;
                        }
                        if (enemyPos.y - myPos.y > 0) {
                            moveY = 1;
                        } else {
                            moveY = -1;
                        }
                        enemy.transform.position = new Vector2(enemyPos.x + moveX, enemyPos.y + moveY);
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
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
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