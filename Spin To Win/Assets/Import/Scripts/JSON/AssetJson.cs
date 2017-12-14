using System;
using UnityEngine;

[Serializable]
public class AssetJson
{
    public string _id;
    public string first_name;
    public string last_name;
    public string bank;
    public string acc_name;
    public string acc_number;
    public string email;
    public string password;
    public int balance;
    public string commision;
    public string role;
}

[Serializable]
public class TableJson
{
    public string _id;
    public string table_name;
    public string table_type;
    public int fee;
}

[Serializable]
public class GameJson
{
    public int balance;
}

public class Responce
{
    public int code;
    public bool success;

    /// <summary>
    /// Parse JSON string
    /// </summary>
    public static Responce<T> CreateFromJSON<T>(string jsonString)
    {
        return JsonUtility.FromJson<Responce<T>>(jsonString);
    }
}

public class Responce<T> : Responce
{
    public T data;
}

public class ResponceTable
{
    public int code;
    public bool success;

    /// <summary>
    /// Parse JSON string
    /// </summary>
    public static ResponceTable<T> CreateFromJSON<T>(string jsonString)
    {

        return JsonUtility.FromJson<ResponceTable<T>>(jsonString);
        
    }
}
public class ResponceTable<T> : Responce
{
    public T[] data;

}



