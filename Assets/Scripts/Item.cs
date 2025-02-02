using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//To create: Right click in project window -> Create -> Inventory item
[CreateAssetMenu(menuName = "Items")]
public class Item : ScriptableObject
{
    public string itemName;
    public float itemValue;

    public ItemType type;

    [System.Serializable]
    public enum ItemType
    {
        Food, Snacks, Drinks, HouseholdGoods, Hygiene, Cleaning, Medicine, Baby, Pet, Stationery, Accessories, Novelty
    }

    public GameObject prefab;

    [ContextMenu("Set Name")]
    public void SetName()
    {
        itemName = name;
    }

}
