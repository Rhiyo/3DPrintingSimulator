using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractionTextScript : MonoBehaviour {

    FirstPersonController player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player.GetInteractableObj() != null && GameObject.FindWithTag("Player").GetComponent<FirstPersonController>() != null)
            GetComponent<Text>().text = "Press 'E': " + player.GetInteractableObj().message;
        else
            GetComponent<Text>().text = "";
    }
}
