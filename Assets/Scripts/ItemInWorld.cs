using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Add this script to all item prefabs, item = the respective InventoryItem scriptable object
public class ItemInWorld : MonoBehaviour
{
    public Item item;

    public void Start()
    {
       SetRandomColour();
    }

    [ContextMenu("Set Random Colour")]
    public void SetRandomColour()
    {
        if (Random.Range(0f, 5f) >= 3)
            GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 0.2f, 0.1f, 0.7f, 0.5f, 1f);
        else
            GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0.45f, 0.9f, 0.1f, 0.7f, 0.5f, 1f);
    }

    //public List<ItemInteraction> itemInteractions;

    //public AudioClip itemCollectedAudio;



    //[System.Serializable]
    //public class ItemInteraction
    //{
    //    // what to do when the item is interacted with

    //}

}
