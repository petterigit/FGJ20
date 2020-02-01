using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public Dictionary<string, int> buttons;
    public float margin;
    public Sprite[] sprites;
    public GameObject button;

    void Start() {
        buttons.Add("Fire1", 0);
        buttons.Add("Fire2", 1);
        buttons.Add("Fire3", 2);
        buttons.Add("Fire4", 3);
    }

    public void GenerateInputs(string[] inputs) {
        float buttonWidth = sprites[0].texture.width;
        float width = inputs.Length * buttonWidth + (inputs.Length - 1) * margin;
        for(int i = 0; i<inputs.Length; i++) {
            if(buttons.TryGetValue(inputs[i].Substring(0, 5), out int iter)) {
                var newButton = Instantiate(button);
                newButton.transform.position = new Vector3(width + i * buttonWidth + (i-1) * margin, 0, 0);
                newButton.transform.SetParent(transform);
                newButton.GetComponent<SpriteRenderer>().sprite = sprites[iter];
            }
        }
    }

    public void DestroyInputs() {
        
    }
}
