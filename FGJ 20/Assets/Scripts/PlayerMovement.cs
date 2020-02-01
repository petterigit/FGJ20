﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	
	public float speed;  
    public string horizontal;
    public string vertical;
    public string firebutton;
    public int dashtime;
    public Animator animator;

    private bool hammertime = false;
    private int cooldown = 10;
    private int speedMultiplier = 3;

    // Update is called once per frame
    void FixedUpdate()
    {
		
        
        Move(speed);
        hammertime = Repair(hammertime);
        GetDash();

        
        
        
    }

    void GetDash() {
        if (Input.GetButton(firebutton)) 
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
            if (cooldown < 120-dashtime) 
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

            if ( (dy != 0) || (dx != 0) ) {
                animator.SetFloat("Speed", 1);
            } else {
                animator.SetFloat("Speed", 0);
            }
            Vector2 newPosition = new Vector2(transform.position.x+dx, transform.position.y+dy);

            transform.position = newPosition;
    }

    bool Repair(bool hammertime) {
        if (Input.GetButton(firebutton)) {
            hammertime = true;
            animator.SetBool("Hammertime", hammertime);
        } else {
            hammertime = false;
            animator.SetBool("Hammertime", hammertime);
        }
        return hammertime;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.tag + " : " + gameObject.name + " : " + Time.time);
        if (col.gameObject.tag == "Boat") {
            speed = 3;
        } else if (col.gameObject.tag == "Plank" ) {
            speed = 2;
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boat" || col.gameObject.tag == "Plank") {
            speed = 1;
        }
    }
    
}