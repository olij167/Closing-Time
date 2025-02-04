using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockPosition : MonoBehaviour
{
    public Item.ItemType itemType;

    public Item item;
    public int numToSpawn = 5;

    public GameObject spawnedItem;

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    public GameObject SpawnStock()
    {
        //Edit to spawn in a line from the front to the back of the shelf based on numToSpawn
        GameObject spawnedObj = Instantiate(item.prefab, transform.position, Quaternion.identity, transform);

        ItemInWorld itemInWorld = spawnedObj.GetComponent<ItemInWorld>();
        itemInWorld.item = item;

        return spawnedItem = spawnedObj;
    }
}
