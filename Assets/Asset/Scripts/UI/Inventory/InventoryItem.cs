using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    Image itemIconImage;
    TextMeshProUGUI amountTxt;

    public Item item;
    public int amount;

    void Awake()
    {
        itemIconImage = transform.GetChild(0).GetComponent<Image>();
        amountTxt = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        UpdateItem();    
    }

    public void UpdateItem()
    {
        if (item == null)
        {
            amountTxt.gameObject.SetActive(false);
        }
        else
        {
            itemIconImage.sprite = item.itemIcon;
            amountTxt.gameObject.SetActive(true);
            amountTxt.text = amount.ToString();
        }
    }
}
