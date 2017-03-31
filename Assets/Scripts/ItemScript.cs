using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {

    public string itemName;

    public int cost = 100;

    public bool energyContainer;

    public bool locked;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public float GetFullCost()
    {
        return cost * transform.localScale.x;
    }
}
