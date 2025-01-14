using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Newtonsoft.Json;

[Serializable]
public class Slot : MonoBehaviour
{
    [SerializeField] private Item _item; public Item item => _item;

    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private Image image;

    [SerializeField] private Image backImage;

    [JsonIgnore] private Inventory inventory;

    public void SetInventory(Inventory newInventory)
    {
        inventory = newInventory;
    }

    public void SetSelectSlot()
    {
        if (_item.itemType != 0)
        {
            inventory.SetSelectSlot(this);
            UpdateDate();
        }
    }

    public void UpdateDate()
    {
        if (_item.itemType != 0)
        {
            if (_item.count > 1)
            {
                text.text = _item.count.ToString();
            }
            else
            {
                text.text = "";
            }

            if (_item.sprite != null)
            {
                image.sprite = _item.sprite;
                image.color = new Color32(255, 255, 255, 255);
            }

            if (inventory.selectSlot == this)
            {
                backImage.color = new Color32(0, 255, 0, 255);
            }
            else
            {
                backImage.color = new Color32(161, 161, 161, 255);
            }
        }
        else
        {
            text.text = "";
            image.color = new Color32(255, 255, 255, 0);
            backImage.color = new Color32(161, 161, 161, 255);
        }


    }

    public SlotDate GetSlotDate()
    {
        SlotDate itemData = new SlotDate(_item.GetItemData());
        return itemData;
    }
}
public class SlotDate
{
    public ItemData _itemData;

    public SlotDate(ItemData itemData)
    {
        _itemData = itemData; ;
    }
}