using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	
	public float speed;  
    public string horizontal;
    public string vertical;
    public string firebutton;
    public Animator animator;

    private bool hammertime = false;

    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		
        float v = Input.GetAxis(vertical);
		float h = Input.GetAxis(horizontal);
		
		float dy = v * speed * Time.deltaTime;
		float dx = h * speed * Time.deltaTime;

        if ( (dy != 0) || (dx != 0) ) {
            animator.SetFloat("Speed", 1);
        } else {
            animator.SetFloat("Speed", 0);
        }

		Vector2 newPosition = new Vector2(transform.position.x+dx, transform.position.y+dy);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, newPosition);
        if (hit.collider != null) {
            //Debug.Log("Ping");
        }
		transform.position = newPosition;

        if (Input.GetButton(firebutton)) {
            //Debug.Log("Ping " + firebutton);
            hammertime = true;
            animator.SetBool("Hammertime", hammertime);
        } else {
            hammertime = false;
            animator.SetBool("Hammertime", hammertime);
        }
        
    }
}