using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHammerAction : MonoBehaviour
{

    public string hammerbutton;
    public PlayerSawingAction psa;

    // Start is called before the first frame update
    void Start()
    {
        // Start action

        // Load animation

        // Set ui
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    // Check for collision
    void OnTriggerStay2D(Collider2D col)
    {
        // Other reqs
        // Not comboing
        if (psa.isComboing) {
            return;
        }
        
        // Check for input
        if (Input.GetButton(hammerbutton)) {
            if (col.gameObject.tag == "Player")
            {
                psa.CancelCombo();
                //var myPos = 
            }
        }
        

    }
}
