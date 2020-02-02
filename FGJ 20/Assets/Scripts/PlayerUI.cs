using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public Dictionary<string, int> buttons;
    public Transform player;
    public Vector3 offset;
    public float margin;
    public float highlightScale;
    public Sprite[] sprites;
    public GameObject button;

    void Start() {
        buttons = new Dictionary<string, int>();
        buttons.Add("Fire1", 0);
        buttons.Add("Fire2", 1);
        buttons.Add("Fire3", 2);
        buttons.Add("Fire4", 3);
    }

    public void GenerateInputs(string[] inputs, int startingIndex) {
        DestroyInputs();

        float buttonWidth = sprites[0].texture.width;
        float ppu = sprites[0].pixelsPerUnit;
        float width = ((inputs.Length - startingIndex) * buttonWidth / ppu  + (inputs.Length - 1 - startingIndex) * margin);
        Debug.Log(width + " " + startingIndex);

        for(int i = startingIndex; i<inputs.Length; i++) {
            if(buttons.TryGetValue(inputs[i].Substring(0, 5), out int iter)) {
                var newButton = Instantiate(button);

                newButton.transform.position = new Vector3(-width / 2 + (i - startingIndex) * buttonWidth / ppu + (i - startingIndex) * margin, 0, 0);
                newButton.transform.position += player.position + offset;
                newButton.transform.SetParent(transform, true);
                newButton.GetComponent<SpriteRenderer>().sprite = sprites[iter];
                if(i == startingIndex) {
                    newButton.transform.localScale *= highlightScale;
                }
            }
        }
    }

    public void DestroyInputs() {
        for(int i=0; i< transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
