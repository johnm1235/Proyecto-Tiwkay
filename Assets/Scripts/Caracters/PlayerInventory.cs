using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public enum ItemType
{
    keys,
    bottles
}

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public Item[] items;
    public event Action OnInventoryChanged;


    private void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        OnInventoryChanged?.Invoke();
    }


    [System.Serializable]
    public class Item
    {
        public string name;
        public bool IsFull;
        public ItemType type;
        public int amount;
        public GameObject SlotSprite;
    }

    [System.Serializable]
    public class ItemData
    {
        public string name;
        public bool isFull;
        public ItemType type;
        public int amount;
    }

    public List<ItemData> GetInventoryData()
    {
        List<ItemData> itemDataList = new List<ItemData>();
        foreach (Item item in items)
        {
            itemDataList.Add(new ItemData
            {
                name = item.name,
                isFull = item.IsFull,
                type = item.type,
                amount = item.amount
            });
        }
        return itemDataList;
    }

    public void LoadInventoryData(List<ItemData> itemDataList)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (i < itemDataList.Count)
            {
                items[i].name = itemDataList[i].name;
                items[i].IsFull = itemDataList[i].isFull;
                items[i].type = itemDataList[i].type;
                items[i].amount = itemDataList[i].amount;
                
            }
        }
    }




}
