using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    void Awake()
    {
        instance = this;
    }

    int capacity = 6;
    List<InventoryItem> items;

    [SerializeField] GameObject InvenoryUI;

    int currentItemIndex = 0;

    void Start()
    {
        items = new List<InventoryItem>(capacity);
        InventoryItem[] invItems = InvenoryUI.GetComponentsInChildren<InventoryItem>();
        foreach(InventoryItem i in invItems) items.Add(i);
    }

    public void AddItem(InventoryItem newItem)
    {
        items.Add(newItem);
    }

    public void RemoveItem(InventoryItem item)
    {
        items.Remove(item);
    }

    public InventoryItem GetItem()
    {
        return items[currentItemIndex];
    }

    public void UpSelect()
    {
        currentItemIndex++;
    }

    public void DownSelect()
    {
        currentItemIndex--;
    }
}
