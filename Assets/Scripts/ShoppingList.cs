using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShoppingList : MonoBehaviour
{
    public List<Item> allPossibleItems;
    public List<Item> availableItemsList;
    public List<ShoppingListItem> shoppingListItems;
    public float listBudget;
    public TextMeshProUGUI listBudgetText;

    public int numOfItems = 3;
    public bool multipleOfSameItem;
    public Vector2 multipleRange = new Vector2(1, 5);

    public GameObject shoppingListContainer;
    public TextMeshProUGUI shoppingListElement;

    public List<ItemInCart> itemsInCart;

    public float totalCost;
    public int itemsRequired;
    public int itemsCollected;

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
    public void GenerateListItems()
    {
        availableItemsList = new List<Item>();

        foreach (Item item in allPossibleItems) { availableItemsList.Add(item); }

        shoppingListItems = new List<ShoppingListItem>();
        for (int i = 0; i < numOfItems; i++)
        {
            if (shoppingListItems.Count >= i)
            {
                Item item = availableItemsList[Random.Range(0, availableItemsList.Count)];

                ShoppingListItem newItem = new ShoppingListItem();
                newItem.item = item;
                newItem.name = item.itemName;

                if (multipleOfSameItem)
                    newItem.numberRequired = (int)Random.Range(multipleRange.x, multipleRange.y + 1);
                else newItem.numberRequired = 1;

                listBudget += newItem.item.itemValue * newItem.numberRequired;
                itemsRequired += newItem.numberRequired;

                shoppingListItems.Add(newItem);
                availableItemsList.Remove(item); //ensure an item isnt selected twice

                TextMeshProUGUI listElement = Instantiate(shoppingListElement, shoppingListContainer.transform);
                listElement.text = item.itemName + " - " + 0 + " / " + newItem.numberRequired;
            }
        }

        listBudgetText.text = "Budget - $" + listBudget;
    }

    public void AddToCart(Item item)
    {
        ItemInCart itemInCart = new ItemInCart();
        itemInCart.item = item;
        itemInCart.name = item.itemName;
        itemInCart.numberCollected = 1;

        bool itemAdded = false;

        for (int i = 0; i < itemsInCart.Count; i++)
        {
            if (itemsInCart[i].item == itemInCart.item)
            {
                itemsInCart[i].numberCollected++;
                totalCost += item.itemValue;

                itemAdded = true;
                break;
            }

            //if (i >= allItemsInCart.Count)
            //{
            //    allItemsInCart.Add(itemInCart);
            //    totalCost += item.itemValue;
            //    itemAdded = true;
            //}
        }

        if (!itemAdded)
        {
            itemsInCart.Add(itemInCart);
            totalCost += item.itemValue;
        }

        CheckList(itemInCart);
    }
    public void CheckList(ItemInCart itemIncart)
    {

        for (int j = 0; j < shoppingListItems.Count; j++)
        {
            if (itemIncart.item == shoppingListItems[j].item)
            {
                itemIncart.isOnList = true;
                //shoppingListItems[j].numberCollected = item.numberCollected;
                itemsCollected++;

                if (itemIncart.numberCollected >= shoppingListItems[j].numberRequired)
                {
                    //Cross off shopping list item
                    shoppingListItems[j].isCollected = true;

                }
                else
                {
                    // UI display num collected/ num required
                    shoppingListItems[j].isCollected = false;
                }

                foreach (Transform c in shoppingListContainer.transform)
                {
                    if (c.GetComponent<TextMeshProUGUI>().text.Contains(itemIncart.item.itemName))
                    {
                        c.GetComponent<TextMeshProUGUI>().text = itemIncart.item.itemName + " - " + itemIncart.numberCollected + 
                            " / " + shoppingListItems[j].numberRequired;
                    }
                }
            }
           
        }

        
    }


}
