using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHammerAction : MonoBehaviour
{

    public float speed = 1;
    public float flyduration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void initFly(Vector3 direction) 
    {
        StartCoroutine(fly(direction));
    }    
    public IEnumerator fly(Vector3 direction)
    {
        // Disable enemy movement
        GetComponent<PlayerMovement>().enabled = false;

        // Cancel their saw combo & disable
        GetComponent<PlayerSawingAction>().CancelCombo();
        GetComponent<PlayerSawingAction>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        
        float t;
        t = flyduration;
        while (t > 0)
        {
            t -= Time.deltaTime;
            float dist = Mathf.SmoothStep(0, speed, flyduration);
            transform.position += dist * direction * Time.deltaTime;
            yield return null;
        }

        // Enable scripts
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerSawingAction>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }    
}
