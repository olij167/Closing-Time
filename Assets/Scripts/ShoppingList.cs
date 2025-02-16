using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class ShoppingList : MonoBehaviour
{
    //public List<Item> allPossibleItems;
    public List<Item> availableItemsList;
    public List<ShoppingListItem> shoppingListItems;
    public float listBudget;
    public TextMeshProUGUI listBudgetText;
    public TextMeshProUGUI totalItemsText;

    public int numOfItems = 3;
    //public bool multipleOfSameItem;
    public Vector2 multipleRange = new Vector2(1, 5);

    public GameObject shoppingListContainer;
    public TextMeshProUGUI shoppingListElement;

    public List<ItemInCart> itemsInCart;

    public float totalCost;
    public int listItemsRequired;
    public int listItemsCollected;
    public int totalItemsCollected;

    [System.Serializable]
    public class ItemInCart
    {
        [HideInInspector] public string name;
        public Item item;
        public int numberCollected;
        public bool isOnList;
    }

    [System.Serializable]
    public class ShoppingListItem
    {
        [HideInInspector] public string name;
        public Item item;
        public int numberRequired;
        //public int numberCollected;
        public bool isCollected;

        public TextMeshProUGUI listElement;
    }

    //public void Start()
    //{
    //    GenerateListItems();

    //    for (int i = 0; i < shoppingListItems.Count; i++)
    //    {
    //        Debug.Log("List Item " + i + " = " + shoppingListItems[i].item.itemName);
    //    }
    //}

    [ContextMenu("GenerateList")]
    public void GenerateListItems(List<Item> itemsInStore)
    {
        availableItemsList = new List<Item>();

        foreach (Item item in itemsInStore) { availableItemsList.Add(item); }

        shoppingListItems.Clear();
        for (int i = 0; i < numOfItems; i++)
        {
            if (shoppingListItems.Count >= i)
            {
                Item item = availableItemsList[Random.Range(0, availableItemsList.Count)];

                ShoppingListItem newItem = new ShoppingListItem();
                newItem.item = item;
                newItem.name = item.itemName;

                //if (multipleOfSameItem)
                //    newItem.numberRequired = (int)Random.Range(multipleRange.x, multipleRange.y + 1);
                //else
                newItem.numberRequired = 1;

                listBudget += newItem.item.itemValue * newItem.numberRequired;
                listItemsRequired += newItem.numberRequired;

                shoppingListItems.Add(newItem);
                availableItemsList.Remove(item); //ensure an item isnt selected twice

                newItem.listElement = Instantiate(shoppingListElement, shoppingListContainer.transform);
                newItem.listElement.text = item.itemName; // + " - " + 0 + " / " + newItem.numberRequired;
            }
        }

        listBudgetText.text = "Budget - $" + listBudget;
        totalItemsText.text = "Number of Items - " + listItemsRequired;
        totalItemsText.transform.SetAsLastSibling();
    }

    public void AddToCart(Item item)
    {
        totalItemsCollected += 1;
        ItemInCart itemInCart = new ItemInCart();
        itemInCart.item = item;
        itemInCart.name = item.itemName;
        itemInCart.numberCollected = 1;

        bool itemAdded = false;

        for (int i = 0; i < itemsInCart.Count; i++)
        {
            if (itemsInCart[i].item == itemInCart.item)
            {
                itemsInCart[i].numberCollected += 1;
                itemInCart.numberCollected = itemsInCart[i].numberCollected;

                totalCost += item.itemValue;

                itemAdded = true;
                Debug.Log("Existing item added to cart");
                break;
            }
        }

        if (!itemAdded)
        {
            itemsInCart.Add(itemInCart);
            Debug.Log("new item added to cart");

            totalCost += item.itemValue;
        }

        CheckList(itemInCart);
    }
    public void CheckList(ItemInCart itemInCart)
    {
        for (int j = 0; j < shoppingListItems.Count; j++)
        {
            if (itemInCart.item.itemName == shoppingListItems[j].item.itemName)
            {
                if (!itemInCart.isOnList)
                    itemInCart.isOnList = true;

                listItemsCollected++;

                shoppingListItems[j].listElement.text = itemInCart.item.itemName + " - " + itemInCart.numberCollected + " / " + shoppingListItems[j].numberRequired;

                if (itemInCart.numberCollected >= shoppingListItems[j].numberRequired)
                {
                    //Cross off shopping list item
                    shoppingListItems[j].listElement.fontStyle = FontStyles.Strikethrough;
                    shoppingListItems[j].isCollected = true;

                }
                else
                {
                    // UI display num collected/ num required
                    shoppingListItems[j].isCollected = false;
                }

                break;
            }
        }
        

        
    }

   
}
