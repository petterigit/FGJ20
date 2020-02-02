using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    public Slider redslider;
    public TextMeshProUGUI redpercentage;
    public Slider blueslider;
    public TextMeshProUGUI bluepercentage;
    public GameObject gameoverbackground;
    public Text gameovertext;
    // Start is called before the first frame update
    
    public void SetHealth(float percentage, int team) {
        if(team == 0) {
            redslider.value = percentage;
            redpercentage.text = Mathf.RoundToInt(percentage * 100).ToString() + "%";
        }
        else {
            blueslider.value = percentage;
            bluepercentage.text = Mathf.RoundToInt(percentage * 100).ToString() + "%";
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void GameOver(int team)
    {
        if (team == 1)
        {
            gameovertext.text = "Red team Won! \nBlue Team Lost: (";
        } else
        {
            gameovertext.text = "Blue team Won! \n Red Team Lost :(";
        }
        
        gameoverbackground.SetActive(true);
        StartCoroutine(waiter());

    }
}

