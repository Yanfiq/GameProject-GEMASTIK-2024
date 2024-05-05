using UnityEngine;

[CreateAssetMenu (fileName = "new Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
}
