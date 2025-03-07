using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class StockManager : MonoBehaviour
{
    //For each section of the store, generate relevant items

    public ShoppingList shoppingList;

    public List<StoreSections> storeSections;

    public List<StockTypes> stockTypes;

    public List<Item> allPossibleItems;

    public List<GameObject> objectsInStore;
    public List<Item> itemsInStore;

    [System.Serializable]
    public class StockTypes
    {
        [HideInInspector] public string elementName;
        public Item.ItemType type;
        public List<Item> itemsOfType;
    }

    [System.Serializable]
    public class StoreSections
    {
        [HideInInspector] public string elementName;
        public Item.ItemType type;
        public List<StockPosition> stockPositions;
        public float sectionRotation;
    }

    // Set the itemType for each stock position based on their placement in the storeSections list
    [ContextMenu("Set Store Sections")]
    public void SetStoreSections()
    {
        //stockTypes = new List<StockTypes>();


        for (int i = 0; i < storeSections.Count; i++)
        {
            storeSections[i].elementName = storeSections[i].type.ToString();

            foreach (StockPosition stockPosition in storeSections[i].stockPositions)
            {
                stockPosition.itemType = storeSections[i].type;
            }

            //StockTypes stock = new StockTypes();
            //stock.type = storeSections[i].type;
            //stock.elementName = storeSections[i].elementName;

            //stockTypes.Add(stock);
        }

    }

    [ContextMenu("Set Stock")]
    public void SetStock()
    {
        DespawnStock();

        //allPossibleItems = shoppingList.allPossibleItems;

        for (int i = 0; i < stockTypes.Count; i++)
        {
            for (int j = 0; j < allPossibleItems.Count; j++)
            {
                if (allPossibleItems[j].type == stockTypes[i].type)
                {
                    if (!stockTypes[i].itemsOfType.Contains(allPossibleItems[j]))
                        stockTypes[i].itemsOfType.Add(allPossibleItems[j]);
                }
            }
        }

        //for each section of the store
        for (int i = 0; i < storeSections.Count; i++)
        {
            //Create a list of all available items for the section
            List<Item> availableItems = new List<Item>();

            for (int j = 0; j < stockTypes.Count; j++)
            {
                if (stockTypes[j].type == storeSections[i].type)
                {
                    foreach (Item item in stockTypes[j].itemsOfType)
                    {
                        availableItems.Add(item);
                        Debug.Log(item.itemName + " added to section: " + stockTypes[j].type);
                    }
                    break;
                }
            }

            //Assign an item to each position in the section
            for (int k = 0; k < storeSections[i].stockPositions.Count; k++)
            {
                if (availableItems.Count > 0)
                {
                    int r = Random.Range(0, availableItems.Count);

                    storeSections[i].stockPositions[k].item = availableItems[r];
                    itemsInStore.Add(availableItems[r]);

                    GameObject stockObj = storeSections[i].stockPositions[k].SpawnStock(storeSections[i].sectionRotation);
                    objectsInStore.Add(stockObj);



                    //remove items from availableItems as they're added to avoid duplicates
                    availableItems.RemoveAt(r);
                }
                else break;
            }

            //spawn multiple items behind each other
        }
    }

    [ContextMenu("Despawn Stock")]
    public void DespawnStock()
    {
        foreach (GameObject go in objectsInStore) { Destroy(go); }

        //Double Check (in case objectsInStore gets cleared prematurely)
        for (int i = 0; i < storeSections.Count; i++)
        {
            for(int j = 0;j < storeSections[i].stockPositions.Count; j++)
            {

                if (storeSections[i].stockPositions[j].spawnedItem != null)
                {
                    Destroy(storeSections[i].stockPositions[j].spawnedItem);
                    storeSections[i].stockPositions[j].spawnedItem = null;
                }

                if (storeSections[i].stockPositions[j].transform.childCount > 0)
                {
                    foreach (Transform c in storeSections[i].stockPositions[j].transform)
                    {
                        if (!c.GetComponent<Canvas>())
                            DestroyImmediate(c.gameObject);
                    }
                }

            }
        }

        objectsInStore = new List<GameObject>();

        itemsInStore = new List<Item>();
    }

    [ContextMenu("Validate Item Names")]
    public void ValidateItemNames()
    {
        RemoveDuplicateItems();


        foreach (Item item in allPossibleItems)
        {
            if (item.itemName != item.name)
                item.itemName = item.name;


        }

    }

    public void RemoveDuplicateItems()
    {
        bool incomplete = false;
        for (int j = 0; j < allPossibleItems.Count; j++)
        {
            if (allPossibleItems[j] == null)
            {
                allPossibleItems.RemoveAt(j);

                incomplete = true;
                break;
            }
            else
            {

                for (int i = 0; i < allPossibleItems.Count; i++)
                {
                    if (allPossibleItems[j] == allPossibleItems[i] && j != i)
                    {
                        allPossibleItems.RemoveAt(i);
                        incomplete = true;
                        break;
                    }


                }

                if (incomplete) break;
            }
        }

        if (incomplete)
            RemoveDuplicateItems();
    }
}
