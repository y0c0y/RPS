using System;
using System.IO;
using System.Text;
using UnityEngine;

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
            userScores = new [] { 0, 0, 0 }
        };
        
        if (File.Exists(_path))
        {
            var fileStream = new FileStream(_path, FileMode.Open); 
            var reader = new StreamReader(fileStream);
            var jsonData = reader.ReadToEnd();
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
        
        var fileStream = new FileStream(_path, FileMode.Create);
        var jsonData = JsonUtility.ToJson(userData);
        var data = Encoding.UTF8.GetBytes(jsonData);
        
        fileStream.Write(data, 0, jsonData.Length);
    }
}
