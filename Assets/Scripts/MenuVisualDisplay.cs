using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVisualDisplay : MonoBehaviour
{
    //Spawn random shop items spaced apart

    //Various effects - rotate, scale, change position

    public List<Item> allItems;
    public int columns;
    public int rows;

    public float columnSpacing;
    public float rowSpacing;
    public Transform startPos;

    public List<GameObject> spawnedItems = new List<GameObject>();

    private void Start()
    {
        SpawnItems();
    }

    [ContextMenu("Spawn Display Items")]
    public void SpawnItems()
    {
        List<Item> availableItems =  new List<Item>();

        availableItems.AddRange(allItems);

        float nextColumnSpace = 0;
        float nextRowSpace = 0;

        //spawn each item in column then move to next row
        for (int r = 0; r < rows; r++)
        {
            //Keep track of where to place each object
            if (r > 0)
             nextRowSpace = r * rowSpacing;

            for (int c = 0; c < columns; c++)
            {
                if (c > 0)
                    nextColumnSpace = c * columnSpacing;
                else nextColumnSpace = 0;

                // spawn  at the appropriate spaced pos

                int i = Random.Range(0, availableItems.Count);

                Vector3 spacedPosition = new Vector3(startPos.position.x + nextColumnSpace, startPos.position.y - nextRowSpace, startPos.position.z);

                Quaternion rand = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));

                spawnedItems.Add(Instantiate(availableItems[i].prefab, spacedPosition, rand, transform));

                spawnedItems[spawnedItems.Count - 1].AddComponent<MenuItem>();

                availableItems.RemoveAt(i);


                if (spawnedItems[spawnedItems.Count - 1].GetComponent<Rigidbody>())
                {
                    spawnedItems[spawnedItems.Count - 1].GetComponent<Rigidbody>().useGravity = false;
                }

                //reset available items if it runs out
                if (availableItems.Count == 0)
                    availableItems.AddRange(allItems);
            }
        }
    }

    [ContextMenu("Clear Spawned Items")]
    public void ClearSpawnedItems()
    {
        foreach (GameObject g in spawnedItems)
        { 
            DestroyImmediate(g); 
        }

        spawnedItems = new List<GameObject>();
    }
}
