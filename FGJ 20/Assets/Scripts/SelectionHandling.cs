using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandling : MonoBehaviour
{

    public GameObject selectionObject;
    private SpriteRenderer selectionsr;
    // Start is called before the first frame update
    void Start()
    {
        selectionsr = selectionObject.GetComponent<SpriteRenderer>();
        selectionObject.SetActive(false);
    }

    public void SetSelection(Vector2 start, Vector2 end) {
        selectionObject.SetActive(true);
        Vector2 middle = (start + end) / 2;
        selectionObject.transform.position = middle;
        selectionsr.size = new Vector2(start.x - end.x, start.y - end.y);
    }

    public void RemoveSelection() {
        selectionObject.SetActive(false);
    }
}
