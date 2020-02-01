using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatHealth : MonoBehaviour
{
    public UIController ui;
    public int team;
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

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(6);
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth -= Mathf.Lerp(sinkingSpeedMax, sinkingSpeedMin, sinkingPercentage) * Time.deltaTime;
        ui.SetHealth(currentHealth / health, team);
        if(currentHealth < 0) {
            // Game over
            GameObject.Find("GameOverBackround").transform.localScale = new Vector3(1, 1, 1);
            // Wait some time and then go back to main menu
            StartCoroutine(waiter());
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        } else
        {
            GameObject.Find("GameOverBackround").transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
