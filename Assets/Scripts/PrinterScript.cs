using UnityEngine;
using System.Collections;

public class PrinterScript : MonoBehaviour {

    private GameObject toClone;
    public Transform spawnPoint;
    public PodiumScript podium;
    public TextMesh warning;
    public TextMesh filamentText;
    public float filament = 1000;
    public Light spotLight;

    public float printTime = 10;
    private float currentPrintTime = 0;
    public bool printing;
    public TextMesh progress;
    private GameObject printingObj;

	// Use this for initialization
	void Start () {
        filamentText.text = filament.ToString();
	}
	
	// Update is called once per frame
	void Update () {

        if (printing)
        {
            float maxTime = printTime * printingObj.transform.localScale.x;
            currentPrintTime += Time.deltaTime;

            printingObj.transform.localScale = new Vector3(printingObj.transform.localScale.x,
                printingObj.transform.localScale.y + Time.deltaTime / maxTime,
                printingObj.transform.localScale.z);
            progress.text = currentPrintTime + " / " + maxTime;
            //Finish Printing
            if(currentPrintTime >= maxTime)
            {
                printingObj.GetComponent<ItemScript>().locked = false;
                printingObj.transform.localScale = new Vector3(printingObj.transform.localScale.x,
                printingObj.transform.localScale.x,
                printingObj.transform.localScale.z);
                float estFilament = filament - printingObj.GetComponent<ItemScript>().cost *
                     printingObj.transform.localScale.x;
                printingObj.transform.position = spawnPoint.transform.position;
                printingObj.GetComponent<Rigidbody>().useGravity = false;
                spotLight.enabled = true;
                printingObj.GetComponent<Interactable>().enabled = true;
                stopPrint();
                filament = estFilament;
                filamentText.text = filament.ToString();
                progress.text = "";
            }
        }else if (printingObj != null)
        {
            printingObj.transform.Rotate(new Vector3(0, 20, 0) * Time.deltaTime);
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().carriedObject !=null)
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().carriedObject.Equals(printingObj))
            {
                spotLight.enabled = false;
                printingObj = null;
                warning.text = "";
            }
        }
	}

    public void StartPrintAnim()
    {
        gameObject.GetComponent<Animator>().SetBool("Printing", true);
    }

    public void EndPrintAnim()
    {
        gameObject.GetComponent<Animator>().SetBool("Printing", false);
    }

    public void Clone()
    {
        if(printing)
            warning.text = "BUSY\n PRINTING!";
        else if(printingObj != null)
        {
            warning.text = "PICKUP\n ITEM!";
        }
        else if (toClone != null)
        {
            if (CheckAfford(toClone.GetComponent<ItemScript>()))
            {
                GameObject spawnedItem = Instantiate(toClone) as GameObject;
                StartPrinting(spawnedItem);
            }
        }
    }

    public GameObject GetClone()
    {
        return toClone;
    }

    public void setClone(GameObject clone)
    {
        this.toClone = clone;
    }

    public void SpawnItem()
    {
        if (printing)
            warning.text = "BUSY\n PRINTING!";
        else if (printingObj != null)
        {
            warning.text = "PICKUP\n ITEM!";
        }
        else if (podium != null) {
            if (CheckAfford(podium.getShownItem().GetComponent<ItemScript>()))
            {
                GameObject spawnedItem = Instantiate(podium.getShownItem()) as GameObject;
                StartPrinting(spawnedItem);
            }
        }
    }

    //Prints the specified object.
    private void StartPrinting(GameObject obj)
    {
        StartPrintAnim();
        printingObj = obj;      
        printingObj.GetComponent<ItemScript>().locked = true;
        printingObj.GetComponent<Interactable>().enabled = false;
        printing = true;
        obj.transform.parent = null;
        obj.transform.position = spawnPoint.transform.position;
        obj.transform.localScale = new Vector3(obj.transform.localScale.x,
            0.2f, obj.transform.localScale.z);
        obj.GetComponent<Rigidbody>().useGravity = true;
        obj.GetComponent<Renderer>().material = new Material(podium.getShownItem().GetComponent<Renderer>().material);
    }

    public void cancelPrint()
    {
        if (printing)
        {
            progress.text = "";
            GameObject.Destroy(printingObj);
            stopPrint();
            printingObj = null;
        }

    }

    public void stopPrint()
    {
        EndPrintAnim();
        printing = false;
        currentPrintTime = 0;
        warning.text = "";
    }

    private bool CheckAfford(ItemScript item)
    {
        float estFilament = filament - item.cost * item.gameObject.transform.localScale.x;
        if (estFilament >= 0)
        {
            return true;
        }

        warning.text = "CAN'T\n AFFORD!";
        return false;
    }
}
