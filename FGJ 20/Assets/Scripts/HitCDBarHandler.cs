using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCDBarHandler : MonoBehaviour
{
    public GameObject bar;
    public GameObject target;
    public Vector3 offset;
    private SpriteRenderer sr;

    private float ogsize;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        ogsize = sr.size.x;
    }

    void Update() {
        bar.transform.position = target.transform.position + offset;
    }

    public void SetBar(float percentage) {
        sr.size = new Vector2(ogsize * percentage, sr.size.y);
    }
}
