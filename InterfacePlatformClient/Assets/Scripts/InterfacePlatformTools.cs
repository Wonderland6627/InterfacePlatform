using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// 用于存储json名称
/// </summary>
public enum JsonType
{
    UnityEditorLocalVersion,
    UnrealEditorLocalVersion,
    GameLibraryLocalVersion,
}

/// <summary>
/// exe版本和本地路径存储
/// </summary>
public class EXEFileLocation
{
    public string filename;
    public string localpath;
}

public class InterfacePlatformTools
{
    public static string JsonPrefix = Application.persistentDataPath + "/JsonData/";

    private static string GetJsonFilePath(JsonType fileName)
    {
        return string.Format("{0}{1}.json", JsonPrefix, fileName.ToString());
    }

    /// <summary>
    /// 读取Json
    /// </summary>
    public static byte[] ReadBytes(JsonType fileName)
    {
        var jsonPath = GetJsonFilePath(fileName);
        var bytes = File.ReadAllBytes(jsonPath);

        return bytes;
    }

    public static string ReadText(JsonType fileName)
    {
        var jsonPath = GetJsonFilePath(fileName);
        var json = File.ReadAllText(jsonPath);

        return json;
    }

    /// <summary>
    /// 写入Json 根据Json类型
    /// </summary>
    public static void WriteBytes(JsonType fileName ,string json)
    {
        var jsonPath = GetJsonFilePath(fileName);
        byte[] bytes = System.Text.Encoding.Default.GetBytes(json);

        File.WriteAllBytes(jsonPath, bytes);
    }
}
