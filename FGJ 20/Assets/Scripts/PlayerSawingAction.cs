using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSawingAction : MonoBehaviour
{
    public string playerid;
    public string[] actionButtons;
    public BoatController enemybc;
    public BoatController bc;
    public InputComboGeneration icg;


    public bool isComboing = false; 
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

    private string[] combolist = new string[0];
    private int comboiter;

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
        if(isComboing) {
            for(int i=0; i<actionButtons.Length; i++) {
                if(Input.GetButtonDown(actionButtons[i])) {
                    if(actionButtons[i] == combolist[comboiter]) {
                        comboiter++;
                        if(comboiter >= combolist.Length) {
                            DoSaw();
                            CancelCombo();
                            break;
                        }
                    }
                    else {
                        CancelCombo();
                        break;
                    }
                }
            }

            

            return;
        }
        // Start sawing
        if(Input.GetButtonDown(actionButtons[0]) && !isCarrying)
        {
            isSawing = true;
            sawStart = transform.position;

        }

        // Stop Sawing
        if (Input.GetButtonUp(actionButtons[0]) && isSawing && !isCarrying)
        {
            isSawing = false;
            sawEnd = transform.position;
        }

        // Start the combo
        if(sawStart != initVector && sawEnd != initVector)
        {
            if(!enemybc.CheckIfInside(sawStart)) {
                sawStart = initVector;
                sawEnd = initVector;
            }
            else if(combolist.Length <= 0) {
                combolist = icg.CreateCombo(playerid, (int)Vector2.Distance(sawStart, sawEnd));
                comboiter = 0;
                isComboing = true;
            }
            
        }

        // Place plank
        if(Input.GetButtonDown(actionButtons[0]) && isCarrying)
        {
            isCarrying = false;
            bc.Place(transform.position, plank);
            plank = null;
        }

    }

    private void DoSaw() {
        plank = enemybc.Saw(sawStart, sawEnd);
        if(plank != null)
        {
            isCarrying = true;
        }
    }

    public void CancelCombo() {
        comboiter = 0;
        combolist = new string[0];
        isComboing = false;

        sawStart = initVector;
        sawEnd = initVector;
    }
}
