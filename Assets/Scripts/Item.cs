using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//To create: Right click in project window -> Create -> Inventory item
[CreateAssetMenu(menuName = "Items")]
public class Item : ScriptableObject
{
    public string itemName;
    public float itemValue;

    public GameObject prefab;

}
