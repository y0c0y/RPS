using System;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class UserData
{
    public int[] userScores = {0,0,0};

    public int this[int draw]
    {
        get => userScores[draw];
        set => userScores[draw] = value;
    }
}

public class DataManager : MonoBehaviour
{
    private string _path;

    private void Start()
    {
        _path = Application.persistentDataPath + "/UserData.json";
    }

    public UserData JsonLoad()
    {
        var data = new UserData
        {
            userScores = new int[] { 0, 0, 0 }
        };
        
        if (File.Exists(_path))
        {
            FileStream fileStream = new FileStream(_path, FileMode.Open); 
            StreamReader reader = new StreamReader(fileStream);
            string jsonData = reader.ReadToEnd();
            fileStream.Close();
            data= JsonUtility.FromJson<UserData>(jsonData);
            
        }
        else
        {
            JsonSave(data);
        }
       
        return data;
    }

    public void JsonSave(UserData userData)
    {
        if (userData == null) throw new ArgumentNullException(nameof(userData));
        
        FileStream fileStream = new FileStream(_path, FileMode.Create);
        
        var jsonData = JsonUtility.ToJson(userData);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, jsonData.Length);
    }
}
