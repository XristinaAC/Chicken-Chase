using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static SaveManager;

public class SaveManager : MonoBehaviour
{
    [Serializable]
    public struct Data_To_Save
    {
        public string Name;
        public int Score;
    }
    [SerializeField] string saveFileName = "";

    public static SaveManager _saveInstance = null;
    [SerializeField] public Data_To_Save PlayerData;
    [SerializeField] public List<Data_To_Save> PlayerData2;

    void Awake()
    {
        if (_saveInstance == null)
        {
            _saveInstance = this;
        }
        else if (_saveInstance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Save_Data();
    }

    public void Save_Data()
    {
        string json_file = JsonUtility.ToJson(PlayerData, true);
        File.WriteAllText(Get_Path(), json_file);
    }

    public void Load_Data()
    {
        if(!File.Exists(Get_Path()))
        {
            Save_Data();
            return;
        }
        string jason_file = File.ReadAllText(Get_Path());
        
        PlayerData = JsonUtility.FromJson<Data_To_Save>(jason_file);
    }

    public void Set_Score(int score)
    {
        if (PlayerData.Score >= score)
        {
            return;
        }

        PlayerData.Score = score;
        
        Save_Data();
    }

    public void Set_Player_Name(string name)
    {
        if(name == PlayerData.Name)
        {
            return;
        }

        PlayerData.Name = name;
        Save_Data();
    }

    public int Get_Score()
    {
        return PlayerData.Score;
    }

    public string Get_Player_Name()
    {
        return PlayerData.Name;
    }

    string Get_Path()
    {
        return Application.persistentDataPath + "/" + saveFileName + ".json";
    }

}
