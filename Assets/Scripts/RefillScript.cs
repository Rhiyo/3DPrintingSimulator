using UnityEngine;
using System.Collections;

public class RefillScript : MonoBehaviour {

    public PrinterScript printer;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ItemScript>() != null)
        {
            if (other.GetComponent<ItemScript>().energyContainer)
            {
                printer.filament += other.GetComponent<ItemScript>().cost;
                printer.filamentText.text = printer.filament.ToString();
                GameObject.Destroy(other.gameObject);
            }
        }
    }
}
