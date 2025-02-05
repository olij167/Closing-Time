using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintStockInfo : MonoBehaviour
{
    public string allItems;

    [System.Serializable]
   public class StockData
    {
        public string itemName;
        public string modelName;
    }

    public List<StockData> stockInfo;

    public StockManager stockManager;

    public float resizeMultiplier;
    public List<GameObject> allPrefabs;

    //public StockInfo infoAsset;

    [ContextMenu("Fill Stock Info")]
    public void FillStockInfo()
    {
        stockInfo = new List<StockData>();
        foreach (Item item in stockManager.allPossibleItems)
        {
            StockData stock = new StockData();
            stock.itemName = item.itemName;
            stock.modelName = item.prefab.name;

            stockInfo.Add(stock);

            allItems += stock.itemName + " - " + stock.modelName + "\n";

        }

        Debug.Log(allItems);


        //infoAsset.stockInfo = stockInfo;
    }

    [ContextMenu("Resize Objects")]
    public void ResizeObjects()
    {
        foreach(GameObject obj in allPrefabs)
        {
            obj.transform.localScale = new Vector3(obj.transform.localScale.x * resizeMultiplier, obj.transform.localScale.y * resizeMultiplier, obj.transform.localScale.z * resizeMultiplier);
        }
    }
    
    //[ContextMenu("Print Stock Info")]
    //public void LogStockInfo()
    //{
    //    foreach (StockInfo info in stockInfo)
    //    {
    //        StockInfo stock = new StockInfo();
    //        stock.itemName = item.itemName;
    //        stock.modelName = item.prefab.name;

    //        stockInfo.Add(stock);
    //    }
    //}
}
