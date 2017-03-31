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
        GetComponent<Text>().text = "";
        if (player.GetInteractableObj() != null && GameObject.FindWithTag("Player").GetComponent<FirstPersonController>() != null)
            if(player.GetInteractableObj().enabled)
                GetComponent<Text>().text = "Press 'E': " + player.GetInteractableObj().message;
            
    }
}
