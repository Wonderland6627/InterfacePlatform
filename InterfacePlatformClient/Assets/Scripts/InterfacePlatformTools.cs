using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
[System.Serializable]
public class EXEFileLocation
{
    public string version;
    public string localpath;

    public EXEFileLocation(string _version, string _localpath)
    {
        version = _version;
        localpath = _localpath;
    }
}

public class InterfacePlatformTools
{
    /// <summary>
    /// https://blog.csdn.net/dl_hum/article/details/17551011?utm_medium=distribute.pc_relevant.none-task-blog-BlogCommendFromMachineLearnPai2-1.add_param_isCf&depth_1-utm_source=distribute.pc_relevant.none-task-blog-BlogCommendFromMachineLearnPai2-1.add_param_isCf
    /// </summary>
    public static string JsonPrefix = Application.persistentDataPath + "/JsonData";

    private static string GetJsonFilePath(JsonType fileName)
    {
        string filePath = string.Format("{0}/{1}.json", JsonPrefix, fileName.ToString());

        return filePath;
    }

    /// <summary>
    /// 初始化检测文件 生成所需json
    /// </summary>
    public static void CreateJsonFiles()
    {
        if (!Directory.Exists(JsonPrefix))
        {
            Directory.CreateDirectory(JsonPrefix);
        }

        var enumsArray = Enum.GetValues(typeof(JsonType));
        for (int i = 0; i < enumsArray.Length; i++)
        {
            var enumType = (JsonType)enumsArray.GetValue(i);
            MakeSureWriteOrRead(enumType);
        }
    }

    /// <summary>
    /// 清理所有json文件
    /// </summary>
    public static void DeleteJsonFiles()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(JsonPrefix);
        foreach (var file in directoryInfo.GetFiles())
        {
            file.Delete();
        }
    }

    /// <summary>
    /// 确保文件的读写
    /// </summary>
    /// <param name="fileName"></param>
    private static void MakeSureWriteOrRead(JsonType fileName)
    {
        string filePath = GetJsonFilePath(fileName);
        if (!File.Exists(filePath))
        {
            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.Close();
            fs.Close();
        }
    }

    /// <summary>
    /// 读取Json
    /// </summary>
    public static string ReadText(JsonType fileName)
    {
        var jsonPath = GetJsonFilePath(fileName);
        MakeSureWriteOrRead(fileName);
        var json = File.ReadAllText(jsonPath);

        return json;
    }

    /// <summary>
    /// 写入Json 根据Json类型
    /// </summary>
    public static void WriteBytes(JsonType fileName, string json)
    {
        var jsonPath = GetJsonFilePath(fileName);
        MakeSureWriteOrRead(fileName);

        File.WriteAllText(jsonPath, json);
    }

    public static T Deserialize<T>(string value)
    {
        return JsonConvert.DeserializeObject<T>(value);
    }

    public static string Serialize<T>(T fileContent)
    {
        return JsonConvert.SerializeObject(fileContent);
    }
}
