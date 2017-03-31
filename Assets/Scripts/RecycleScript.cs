using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RecycleScript : MonoBehaviour
{

    public Transform spawn;

    public GameObject filament;

    private List<ItemScript> toRecycle = new List<ItemScript>();

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ItemScript>() != null)
        {
            if (!other.GetComponent<ItemScript>().energyContainer)
            {
                if(!toRecycle.Contains(other.gameObject.GetComponent<ItemScript>()))
                    toRecycle.Add(other.gameObject.GetComponent<ItemScript>());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ItemScript>() != null)
        {
            if (!other.GetComponent<ItemScript>().energyContainer)
            {
                toRecycle.Remove(other.GetComponent<ItemScript>());
            }
        }
    }

    public void Recycle()
    {
        float totalEnergy = 0;

        foreach(ItemScript item in toRecycle)
        {
            totalEnergy += item.GetFullCost();
            if (item != null)
                if (item.gameObject != null)
                {
                    GameObject.Destroy(item.gameObject);
                }
        }
        toRecycle.Clear();

        if (totalEnergy > 0)
        {
            GameObject newFilament = Instantiate(filament);
            newFilament.transform.position = spawn.transform.position;
            newFilament.GetComponent<ItemScript>().cost = Mathf.RoundToInt(totalEnergy);
            newFilament.GetComponent<ItemScript>().energyContainer = true;
            newFilament.GetComponent<FilamentScript>().Set();
        }
    }
}
