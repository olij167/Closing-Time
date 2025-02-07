using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class ItemScanner : MonoBehaviour
{
    //Spawn Items on a conveyor belt
    //Move items along conveyor belt
    //Scan when going through trigger
    //Add total cost on a tracker
    //add item to a reciept
    //Add shopper score to reciept

    public ShoppingList shoppingList;
    public Transform spawnPos;
    public float spawnDelay;

    public List<SpawnQueueItem> spawnQueue = new List<SpawnQueueItem>();

    public TextMeshProUGUI costTrackerText;

    public TextMeshProUGUI receiptItemPrefab;
    public GameObject receiptContainer;

    public float receiptRunningTotal;
    public List<TextMeshProUGUI> receiptList = new List<TextMeshProUGUI>();

    public List<GameObject> listedItems = new List<GameObject>();

    private AudioSource audioSource;
    public AudioClip scannerAudioClip;
    public Scrollbar receiptScrollbar;

    public bool showStats;

    public Color receiptTextColour;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    [System.Serializable]
    public class SpawnQueueItem
    {
        public Item queuedItem;
        public float spawnDelay;
        public bool isOnList;
    }

    public void SpawnItemsEndGame()
    {
        //spawn each item in the players cart one by one
        for (int i = 0; i < shoppingList.itemsInCart.Count; i++) 
        {
            for (int j = 0; j < shoppingList.itemsInCart[i].numberCollected; j++)
            {
                SpawnQueueItem sqItem = new SpawnQueueItem();
                sqItem.queuedItem = shoppingList.itemsInCart[i].item;

                for (int k = 0; k < shoppingList.shoppingListItems.Count; k++)
                {
                    if (shoppingList.itemsInCart[i].item == shoppingList.shoppingListItems[k].item)
                    {
                        if (j <= shoppingList.shoppingListItems[k].numberRequired)
                            sqItem.isOnList = true;
                    }
                }

                if (spawnQueue.Count == 0f)
                {
                    sqItem.spawnDelay = spawnDelay;
                }
                else
                {
                    sqItem.spawnDelay = spawnDelay + spawnQueue[spawnQueue.Count - 1].spawnDelay;
                }

                spawnQueue.Add(sqItem);
            }
        }

        for (int i = 0; i < spawnQueue.Count; i++)
        {
            StartCoroutine(SpawnWithDelay(spawnQueue[i].queuedItem, spawnQueue[i].spawnDelay, spawnQueue[i].isOnList));
        }

    }

    public IEnumerator SpawnWithDelay(Item item, float delay, bool isOnList)
    {
        yield return new WaitForSeconds(delay);
        GameObject obj = Instantiate(item.prefab, spawnPos.position, Quaternion.identity);
        obj.GetComponent<ItemInWorld>().item = item;

        if (isOnList)
            listedItems.Add(obj);

    }

    public void ScanItem(ItemInWorld itemInWorld)
    {
        // Add to total cost tracker
        receiptRunningTotal += itemInWorld.item.itemValue;
        costTrackerText.text = "$" + receiptRunningTotal.ToString("00.00");
        // print info onto receipt
        audioSource.PlayOneShot(scannerAudioClip);

        //itemInWorld.GetComponent<Collider>().enabled = false;

        PrintToReceipt(itemInWorld);

        if (receiptList.Count == shoppingList.totalItemsCollected)
        {
            showStats = true;
        }

    }

    public void PrintToReceipt(ItemInWorld itemInWorld)
    {
        TextMeshProUGUI rItem = Instantiate(receiptItemPrefab, receiptContainer.transform);
        rItem.text = itemInWorld.item.itemName + " . . . $" + itemInWorld.item.itemValue.ToString("00.00");
        if (listedItems.Contains(itemInWorld.gameObject))
        {
            rItem.color = receiptTextColour;
        }

        receiptList.Add(rItem);
        receiptScrollbar.value = 0;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ItemInWorld>())
        {
            ScanItem(other.GetComponent<ItemInWorld>());
        }
    }
}
