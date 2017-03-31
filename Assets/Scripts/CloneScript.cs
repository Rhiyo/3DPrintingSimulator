using UnityEngine;
using System.Collections;

public class CloneScript : MonoBehaviour {

    public PrinterScript printer;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ItemScript>() != null && printer != null)
        {
            if(!other.GetComponent<ItemScript>().energyContainer)
                printer.setClone(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ItemScript>() != null && printer != null)
        {
            if (printer.GetClone() != null)
            {
                if (printer.GetClone().Equals(other.gameObject))
                {
                    printer.setClone(null);
                }
            }
        }
    }
}
