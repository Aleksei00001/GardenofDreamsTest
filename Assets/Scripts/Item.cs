using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item : MonoBehaviour
{
    [SerializeField] private int _itemType; public int itemType => _itemType;
    [SerializeField] private Sprite _sprite; public Sprite sprite => _sprite;
    [SerializeField] private int _count; public int count => _count;
    [SerializeField] private int _itemID; public int itemID => _itemID;
    [SerializeField] private float _damage; public float damage => _damage;
    [SerializeField] private float _attackeSpeed; public float attackeSpeed => _attackeSpeed;
    [SerializeField] private int _useBulletID; public int useBulletID => _useBulletID;

    public void AddCount(int addCount)
    {
        _count += addCount;
    }

    public void SetItem(Item newItem)
    {
        _itemType = newItem.itemType;
        _sprite = newItem.sprite;
        _count = newItem.count;
        _itemID = newItem.itemID;
        _damage = newItem.damage;
        _attackeSpeed = newItem.attackeSpeed;
        _useBulletID = newItem.useBulletID;
    }
    public void SetItem(ItemData itemData)
    {
        _itemType = itemData._itemType;
        _count = itemData._count;
        _itemID = itemData._itemID;
        _damage = itemData._damage;
        _attackeSpeed = itemData._attackeSpeed;
        _useBulletID = itemData._useBulletID;
    }

    public void RemoveItem()
    {
        _itemType = 0;
        _sprite = null;
        _count = 0;
        _itemID = 0;
        _damage = 0;
        _attackeSpeed = 0;
        _useBulletID = 0;
    }

    public Item(){}

    public Item(ItemData itemData)
    {
        _itemType = itemData._itemType;
        _count = itemData._count;
        _itemID = itemData._itemID;
        _damage = itemData._damage;
        _attackeSpeed = itemData._attackeSpeed;
        _useBulletID = itemData._useBulletID;
    }


    public Item(int itemType, int count, int itemID, float damage, float attackeSpeed, int useBulletID)
    {
        _itemType = itemType;
        _count = count;
        _itemID = itemID;
        _damage = damage;
        _attackeSpeed = attackeSpeed;
        _useBulletID = useBulletID;
    }

    public void SetSprite(Sprite newSprite)
    {
        _sprite = newSprite;
    }

    public ItemData GetItemData()
    {
        ItemData itemData = new ItemData(_itemType, _count, _itemID, _damage, _attackeSpeed, _useBulletID);
        return itemData;
    }
}

public class ItemData
{
    public int _itemType;
    public int _count;
    public int _itemID;
    public float _damage;
    public float _attackeSpeed;
    public int _useBulletID;

    public ItemData(int itemType, int count, int itemID, float damage, float attackeSpeed, int useBulletID) 
    {
        _itemType = itemType;
        _count = count;
        _itemID = itemID;
        _damage = damage;
        _attackeSpeed = attackeSpeed;
        _useBulletID = useBulletID;
    }
}
