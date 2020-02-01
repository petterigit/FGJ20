using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSawingAction : MonoBehaviour
{
    public string actionButton;
    public BoatController enemybc;
    public BoatController bc;

    [SerializeField]
    private bool isSawing = false;
    [SerializeField]
    private bool isCarrying = false;

    [SerializeField]
    private Vector2 sawStart;
    [SerializeField]
    private Vector2 sawEnd;
    [SerializeField]
    private Sprite plank = null;

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
        // Start sawing
        if(Input.GetButtonDown(actionButton) && !isCarrying)
        {
            isSawing = true;
            sawStart = transform.position;

        }

        // Stop Sawing
        if (Input.GetButtonUp(actionButton) && isSawing && !isCarrying)
        {
            isSawing = false;
            sawEnd = transform.position;
        }

        // Do the sawing
        if(sawStart != initVector && sawEnd != initVector)
        {
            plank = enemybc.Saw(sawStart, sawEnd);
            if(plank != null)
            {
                isCarrying = true;
            }

            sawStart = initVector;
            sawEnd = initVector;
        }

        // Place plank
        if(Input.GetButton(actionButton) && isCarrying)
        {
            isCarrying = false;
            bc.Place(transform.position, plank);
            plank = null;
        }

    }    
}
