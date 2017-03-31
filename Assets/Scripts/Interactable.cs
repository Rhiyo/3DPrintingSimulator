using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

    public UnityEvent onDown;
    public UnityEvent onHold;
    public UnityEvent onUp;

    public string message = "Press 'E' to interact.";
    public float interactionDistance = 2;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            FirstPersonController player = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
            Interactable obj = player.GetInteractableObj();
            if(this == obj)
            {
                if(isPickupable)
                    player.PickupObject();
                else
                {

                }
            }
        }
        */
        FirstPersonController player = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        Interactable obj = player.GetInteractableObj();
        if (this == obj)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {

                onUp.Invoke();

            }
            if (Input.GetKey(KeyCode.E))
            {

                onHold.Invoke();

            }
            if (Input.GetKeyDown(KeyCode.E))
            {   

                onDown.Invoke();

            }
        }
	}
}
