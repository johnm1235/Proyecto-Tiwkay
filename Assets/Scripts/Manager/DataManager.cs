using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    private string filePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerData()
    {
        PlayerData data = new PlayerData();
        data.inventory = PlayerInventory.Instance.GetInventoryData();
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
    }

    public void LoadPlayerData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            PlayerInventory.Instance.LoadInventoryData(data.inventory);
        }
    }

    public void ClearPlayerData()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

}

[System.Serializable]
public class PlayerData
{
    public List<PlayerInventory.ItemData> inventory;
}
