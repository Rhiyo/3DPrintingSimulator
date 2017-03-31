using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LeftButtonScript : MonoBehaviour {
    public GameObject podium;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void onClick()
    {
        podium.GetComponent<PodiumScript>().PrevItem();
    }

}
