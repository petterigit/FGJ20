using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatHealth : MonoBehaviour
{
    public float health;
    [SerializeField] private float currentHealth;
    public float sinkingSpeedMin;
    public float sinkingSpeedMax;
    public float sinkingPercentage;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        sinkingPercentage = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        health -= Mathf.Lerp(sinkingSpeedMax, sinkingSpeedMin, sinkingPercentage) * Time.deltaTime;
        if(health < 0) {
            

            // Show button
            GameObject.Find("GameOverBackround").transform.localScale = new Vector3(1, 1, 1);
        } else
        {
            GameObject.Find("GameOverBackround").transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
