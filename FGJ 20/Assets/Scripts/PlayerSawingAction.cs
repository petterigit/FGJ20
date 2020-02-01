using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSawingAction : MonoBehaviour
{
    public string actionButton;
    public BoatController bc;

    [SerializeField]
    private bool isSawing = false;

    [SerializeField]
    private Vector2 sawStart;
    [SerializeField]
    private Vector2 sawEnd;

    private Camera cam;

    private Vector2 initVector;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        initVector = new Vector2(0, 0);
        sawStart = initVector;
        sawEnd = initVector;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(actionButton))
        {
            isSawing = true;
            sawStart = transform.position;

        }

        if (Input.GetButtonUp(actionButton) && isSawing)
        {
            isSawing = false;
            sawEnd = transform.position;
        }

        if(sawStart != initVector && sawEnd != initVector)
        {
            bc.Saw(sawStart, sawEnd);
            sawStart = initVector;
            sawEnd = initVector;
        }

    }    
}
