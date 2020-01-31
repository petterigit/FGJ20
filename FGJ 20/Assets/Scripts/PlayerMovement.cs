using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	
	public float speed;  
    public string horizontal;
    public string vertical;

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
		
		transform.position = new Vector2(transform.position.x+dx, transform.position.y+dy);
    }
	
}