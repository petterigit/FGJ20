using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIController : MonoBehaviour
{
    public Slider redslider;
    public TextMeshProUGUI redpercentage;
    public Slider blueslider;
    public TextMeshProUGUI bluepercentage;
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
}
