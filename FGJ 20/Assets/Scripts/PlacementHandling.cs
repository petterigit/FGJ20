using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementHandling : MonoBehaviour
{
    public GameObject placementObject;
    public SpriteRenderer placementsr;

    public void SetPlacement(Vector2 pos, Vector2 size) {
        placementObject.SetActive(true);
        placementObject.transform.position = pos;
        placementsr.size = size;
    }

    public void MovePlacement(Vector2 pos) {
        placementObject.transform.position = pos;
    }

    public void RemovePlacement() {
        placementObject.SetActive(false);
    }
}
