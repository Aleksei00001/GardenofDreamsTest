using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class Inventory : MonoBehaviour
{


    [SerializeField] private List<Slot> Slots;

    [SerializeField] private Slot _selectSlot; public Slot selectSlot => _selectSlot;

    [SerializeField] private GameObject buttonRemove;
    [SerializeField] private GameObject buttonSelect;

    [SerializeField] private List<SpriteFindData> spriteFindDatas;


    [Serializable]
    public class SpriteFindData
    {
        public int idSprite;
        public Sprite sprite;
    }

    [JsonConstructor]
    public Inventory(List<Slot> newSlots)
    {
        Slots = newSlots;
    }

    public Inventory(){ }


    private void Update()
    {
        if (_selectSlot == null)
        {
            buttonRemove.SetActive(false);
            buttonSelect.SetActive(false);
        }
        else
        {
            buttonRemove.SetActive(true);
            if (_selectSlot.item.itemType == 3)
            {
                buttonSelect.SetActive(true);
            }
            else
            {
                buttonSelect.SetActive(false);
            }
        }
    }

    private void Start()
    {
        foreach (var itemSlots in Slots)
        {
            itemSlots.SetInventory(this);
            itemSlots.UpdateDate();
        }
        this.gameObject.SetActive(false);
    }

    public bool AddItem(Item newItem)
    {
        Slot addSlot = null;
        if (newItem.itemType == 1 || newItem.itemType == 3)
        {
            addSlot = FindFreeSlot();
            if (addSlot != null)
            {
                AddItem(addSlot, newItem);
                return true;
            }
        }
        else if (newItem.itemType == 2)
        {
            addSlot = FindItemID(newItem);
            if (addSlot != null)
            {
                AddItemCount(addSlot, newItem.count);
                return true;
            }
            else
            {
                addSlot = FindFreeSlot();
                if (addSlot != null)
                {
                    AddItem(addSlot, newItem);
                    return true;
                }
            }
        }
        return false;
    }

    public Slot FindItemID(Item findItem)
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].item.itemID == findItem.itemID)
            {
                return Slots[i];
            }
        }
        return null;
    }
    public Slot FindItemID(int findItemID)
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].item.itemID == findItemID)
            {
                return Slots[i];
            }
        }
        return null;
    }

    public Slot FindFreeSlot()
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].item.itemType == 0)
            {
                return Slots[i];
            }
        }
        return null;
    }

    public void AddItemCount(Slot selectSlot, int addCount)
    {
        selectSlot.item.AddCount(addCount);
        selectSlot.UpdateDate();
    }

    public void AddItem(Slot selectSlot, Item addItem)
    {
        selectSlot.item.SetItem(addItem);
        selectSlot.UpdateDate();
    }

    public void RemoveItem(Slot selectSlot)
    {
        selectSlot.item.RemoveItem();
        if (selectSlot == _selectSlot)
        {
            _selectSlot = null;
        }
        selectSlot.UpdateDate();
    }

    public void RemoveItemButton()
    {
        _selectSlot.item.RemoveItem();
        selectSlot.UpdateDate();
        _selectSlot = null;
    }

    public void SetSelectSlot(Slot newSelectSlot)
    {
        Slot tempUpdaetSlot;
        tempUpdaetSlot = _selectSlot;
        _selectSlot = newSelectSlot;
        _selectSlot.UpdateDate();
        if (tempUpdaetSlot != null)
        {
            tempUpdaetSlot.UpdateDate();
        }
    }

    public void LoadInventoryDate(InventoryDate newInwentory)
    {
        foreach (var itemSlot in Slots)
        {
            RemoveItem(itemSlot);
        }
        foreach (var itemSlot in newInwentory._slots)
        {
            Item newItem = new Item(itemSlot._itemData);
            newItem.SetSprite(FindSpriteForID(newItem.itemID));
            AddItem(newItem);
        }
    }

    public Sprite FindSpriteForID(int itemID)
    {
        foreach (var itemSpriteFindDatas in spriteFindDatas)
        {
            if (itemSpriteFindDatas.idSprite == itemID)
            {
                return itemSpriteFindDatas.sprite;
            }
        }
        return null;
    }

    public InventoryDate GetInventoryDate()
    {
        InventoryDate inventoryDate = new InventoryDate(Slots);
        return inventoryDate;
    }
}
public class InventoryDate
{
    public List<SlotDate> _slots = new List<SlotDate>();

    public InventoryDate(List<Slot> slots)
    {
        foreach (var itemSlots in slots)
        {
            _slots.Add(itemSlots.GetSlotDate());
        }
    }

    public InventoryDate(){ }
}