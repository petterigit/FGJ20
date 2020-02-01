using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComboGeneration : MonoBehaviour
{
    public string[] inputs;
    public int minComboLength;
    public int maxComboLength;
    
    
    public string[] CreateCombo(string playerid, int length) {
        
        length = Mathf.Clamp(length, minComboLength, maxComboLength);
        string[] combo = new string[length];

        for(int i = 0; i < length; i++) {
            combo[i] = inputs[Random.Range(0, inputs.Length)] + "_" + playerid;
        }
        
        foreach(var i in combo) {
            Debug.Log(i);
        } 
        return combo;
    }
    
}
