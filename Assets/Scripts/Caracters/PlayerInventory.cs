using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    keys,
    bottles
}



public class PlayerInventory : MonoBehaviour
    
{
    public static PlayerInventory Instance;
    public Item[] items;
    private void Awake()
    {
        Instance = this;
    }

    public void EmptyStlo()
    {

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

}
