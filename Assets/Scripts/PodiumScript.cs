/*
Rotates item on podium for selection.
*/

using UnityEngine;
using System.Collections;

public class PodiumScript : MonoBehaviour {

    public GameObject[] items;
    public GameObject offScreen;
    public GameObject onScreen;
    public Camera offCam;

    public Material skinMaterial;
    public Material metalMat;
    public Material furMat;

    public GameObject spawnPoint;

    public TextMesh text;

    public TextMesh cost;

    public float rotationSpeed = 5;
    private Vector3 spinVector;
    private int currentItemIndex = 0;
    private GameObject shownItem;
    private float scale = 1;
    private Color itemColour = new Color(1, 0.87450980392f, 0.76862745098f, 1);
    private Material chosenMat;

	// Use this for initialization
	void Start () {
        spinVector = new Vector3(0, rotationSpeed, 0);
        shownItem = newItem(Instantiate(items[0]) as GameObject);
        shownItem.GetComponent<Renderer>().material = skinMaterial;
        text.text = shownItem.GetComponent<ItemScript>().itemName;
        cost.text = (shownItem.GetComponent<ItemScript>().cost * scale).ToString();
        chosenMat = skinMaterial;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Cancel")){
            offScreen.SetActive(false);
            onScreen.SetActive(true);
            GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().enabled = true;
            offCam.enabled = false;
        }

        if (transform != null)
        {
            transform.Rotate(spinVector * Time.deltaTime);
        }
	}

    public void PrevItem()
    {
        if (currentItemIndex == 0)
            currentItemIndex = items.Length - 1;
        else
            currentItemIndex--;

        RefreshItem();
    }

    public void NextItem()
    {
       if (currentItemIndex == items.Length - 1)
            currentItemIndex = 0;
        else
            currentItemIndex++;

        RefreshItem();
    }

    public void Scale(float times)
    {
        shownItem.transform.localScale = new Vector3(times, times, times);
        scale = times;
        cost.text = (shownItem.GetComponent<ItemScript>().cost * scale).ToString();
    }

    public void Activate()
    {
        offScreen.SetActive(true);
        onScreen.SetActive(false);
        Cursor.visible = true;
    }

    public void normalColour()
    {
        itemColour = new Color(1, 0.87450980392f, 0.76862745098f, 1);
        shownItem.GetComponent<Renderer>().material.color = itemColour;
    }

    public void redColour()
    {
        itemColour = Color.red;
        shownItem.GetComponent<Renderer>().material.color = itemColour;
    }

    public void greenColour()
    {
        itemColour = Color.green;
        shownItem.GetComponent<Renderer>().material.color = itemColour;
    }

    public void blueColour()
    {
        itemColour = Color.blue;
        shownItem.GetComponent<Renderer>().material.color = itemColour;
    }

    public void setColor()
    {
        shownItem.GetComponent<Renderer>().material = new Material(shownItem.GetComponent<Renderer>().material);
        shownItem.GetComponent<Renderer>().material.color = itemColour;
    }

    public void skinTexture()
    {
        itemColour = new Color(1, 0.87450980392f, 0.76862745098f, 1);
        shownItem.GetComponent<Renderer>().material.color = itemColour;
    }

    private void RefreshItem()
    {
        if(shownItem !=null)
            Destroy(shownItem);
            shownItem = newItem(Instantiate(items[currentItemIndex]) as GameObject);
        text.text = shownItem.GetComponent<ItemScript>().itemName;
        cost.text = (shownItem.GetComponent<ItemScript>().cost * scale).ToString();
    }

    public void SkinTexture()
    {
        chosenMat = skinMaterial;
        shownItem.GetComponent<Renderer>().material = skinMaterial;
        shownItem.GetComponent<Renderer>().material.color = itemColour;
    }

    public void FurTexture()
    {
        chosenMat = furMat;
        shownItem.GetComponent<Renderer>().material = furMat;
        shownItem.GetComponent<Renderer>().material.color = itemColour;
    }

    public void MetalTexture()
    {
        chosenMat = metalMat;
        shownItem.GetComponent<Renderer>().material = metalMat;
        shownItem.GetComponent<Renderer>().material.color = itemColour;
    }

    private GameObject newItem(GameObject item)
    {
        item.transform.parent = transform;
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one * scale;
        item.GetComponent<Renderer>().material = chosenMat;
        item.GetComponent<Renderer>().material.color = itemColour;
        item.GetComponent<Rigidbody>().useGravity = false;
        return item;
    }

    public GameObject getShownItem()
    {
        return shownItem;
    }


}
