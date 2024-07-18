using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int highScore;
    public string highScorePlayerName;

}

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
            filePath = Application.persistentDataPath + "/playerData.json";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
        Debug.Log("Datos guardados en: " + filePath);
    }

    public PlayerData LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Datos cargados desde: " + filePath);
            return data;
        }
        else
        {
            Debug.LogWarning("Archivo no encontrado en: " + filePath);
            return new PlayerData();
        }
    }
}
