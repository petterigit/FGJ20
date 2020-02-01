using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPlayerMovement : MonoBehaviour
{

    public float speed;
    public string horizontal;
    public string vertical;
    public string hammerbutton;
    public string menubutton;
    public string dashbutton;
    public int dashtime;
    public Animator animator;
    public Rigidbody2D rb;

    private int cooldown = 10;
    private int speedMultiplier = 3;
    private string area = "none";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        OnClickMenu(area);
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

    void OnClickMenu(string area)
    {
        if (Input.GetButton(menubutton))
        {
            if (area == "PlayButton")
            {
                Debug.Log("is play time");
                SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);

            }
            else if (area == "InfoButton")
            {
                Debug.Log("is info time");
            }
            else if (area == "ExitButton")
            {
                Debug.Log("is exit time");
                Application.Quit();
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.tag + " : " + gameObject.name + " : " + Time.time);
        if (col.gameObject.tag == "PlayButton")
        {
            Debug.Log("setting area: play");
            area = "PlayButton";
        }
        else if (col.gameObject.tag == "InfoButton")
        {
            Debug.Log("setting area: info");
            area = "InfoButton";
        }
        else if (col.gameObject.tag == "ExitButton")
        {
            Debug.Log("setting area: exit");
            area = "ExitButton";
        }
        else
        {
            Debug.Log("setting area: none");
            area = "none";
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boat" || col.gameObject.tag == "Plank" || col.gameObject.tag == "PlayButton" || col.gameObject.tag == "InfoButton" || col.gameObject.tag == "ExitButton")
        {
            //speed = 1;
            area = "none";
        }
    }

}