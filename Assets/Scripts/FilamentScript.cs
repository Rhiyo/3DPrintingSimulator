using UnityEngine;
using System.Collections;

public class FilamentScript : MonoBehaviour {

    public ItemScript item;
    public TextMesh text;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Set()
    {
        text.text = item.cost.ToString();
    }
}
