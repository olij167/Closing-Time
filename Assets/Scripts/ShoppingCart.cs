//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEditorInternal.Profiling.Memory.Experimental;
//using UnityEngine;
//using static UnityEditor.Progress;

//public class ShoppingCart : MonoBehaviour
//{
//    private ShoppingList shoppingList;

//   public List<ItemInCart> itemsInCart;
//   public float totalCost;

//    [System.Serializable]
//    public class ItemInCart
//    {
//        [HideInInspector] public string name;
//        public Item item;
//        public int numberCollected;
//    }

//    private void Start()
//    {
//        shoppingList = FindObjectOfType<ShoppingList>();
//    }

//    public void AddToCart(Item item)
//    {
//        ItemInCart itemInCart = new ItemInCart();
//        itemInCart.item = item;
//        itemInCart.name = item.itemName;
//        itemInCart.numberCollected = 1;

//        bool itemAdded = false;

//        for(int i = 0; i < itemsInCart.Count; i++)
//        {
//            if (itemsInCart[i].item == itemInCart.item)
//            {
//                itemsInCart[i].numberCollected++;
//                totalCost += item.itemValue;

//                itemAdded = true;
//                break;
//            }

//            if (i >= itemsInCart.Count)
//            {
//                itemsInCart.Add(itemInCart);
//                totalCost += item.itemValue;
//                itemAdded = true;
//            }
//        }

//        if (!itemAdded)
//        {
//            itemsInCart.Add(itemInCart);
//            totalCost += item.itemValue;
//        }

//        CheckList();
//    }

//    public void CheckList()
//    {
//        for(int i = 0;i < itemsInCart.Count;i++)
//        {
//            for (int j = 0; j < shoppingList.shoppingListItems.Count; j++)
//            {
//                if (itemsInCart[i].item == shoppingList.shoppingListItems[j].item)
//                {
//                    shoppingList.shoppingListItems[j].numberCollected = itemsInCart[i].numberCollected;

//                    foreach(Transform c in shoppingList.shoppingListContainer.transform)
//                    {
//                        if (c.GetComponent<TextMeshProUGUI>().text.Contains(itemsInCart[i].item.itemName))
//                        {
//                            c.GetComponent<TextMeshProUGUI>().text = itemsInCart[i].item.itemName + " - " + shoppingList.shoppingListItems[j].numberCollected + " / " + shoppingList.shoppingListItems[j].numberRequired;
//                        }
//                    }

//                    if (itemsInCart[i].numberCollected >= shoppingList.shoppingListItems[j].numberRequired)
//                    {
//                        //Cross off shopping list item
//                        shoppingList.shoppingListItems[j].isCollected = true;

//                    }
//                    else
//                    {
//                        // UI display num collected/ num required
//                        shoppingList.shoppingListItems[j].isCollected = false;
//                    }
//                }
//            }
//        }
//    }
//}
