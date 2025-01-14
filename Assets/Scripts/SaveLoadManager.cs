using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;

[Serializable]
public class SaveLoadManager : MonoBehaviour
{
    private string filePathInventory;
    private string filePathPlayer;
    private string filePathEnemySpawner;
    [SerializeField] public Inventory inventory;
    [SerializeField] public Player player;
    [SerializeField] public EnemySpawner enemySpawner;

    void Start()
    {
        filePathInventory = Application.persistentDataPath + "/Inventory.json";
        filePathPlayer = Application.persistentDataPath + "/Player.json";
        filePathEnemySpawner = Application.persistentDataPath + "/EnemySpawner.json";
    }


    public void SaveData()
    {
        string json;
        json = JsonConvert.SerializeObject(inventory.GetInventoryDate());
        File.WriteAllText(filePathInventory, json);
        json = JsonConvert.SerializeObject(player.GetPlayerDate());
        File.WriteAllText(filePathPlayer, json);
        json = JsonConvert.SerializeObject(enemySpawner.GetEnemySpawnerDate());
        File.WriteAllText(filePathEnemySpawner, json);
    }

    public void LoadData()
    {
        string json;
        if (File.Exists(filePathInventory))
        {
            json = File.ReadAllText(filePathInventory);
            InventoryDate deserializedInventory = JsonConvert.DeserializeObject<InventoryDate>(json);
            inventory.LoadInventoryDate(deserializedInventory);
        }
        if (File.Exists(filePathPlayer))
        {
            json = File.ReadAllText(filePathPlayer);
            PlayerDate deserializedSlot = JsonConvert.DeserializeObject<PlayerDate>(json);
            player.LoadPlayerDate(deserializedSlot);
        }
        if (File.Exists(filePathEnemySpawner))
        {
            json = File.ReadAllText(filePathEnemySpawner);
            EnemySpawnerDate deserializedSlot = JsonConvert.DeserializeObject<EnemySpawnerDate>(json);
            enemySpawner.LoadEnemySpawnerDate(deserializedSlot);
        }
    }

}
