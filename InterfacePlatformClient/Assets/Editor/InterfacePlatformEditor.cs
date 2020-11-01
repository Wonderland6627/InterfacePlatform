using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;

public class InterfacePlatformEditor 
{
    [MenuItem("开发工具/打开本地存储目录")]
    public static void OpenLocationRecodePath()
    {
        string jsonLocationPath = InterfacePlatformTools.JsonPrefix;
        Process.Start(jsonLocationPath);
    }

    [MenuItem("开发工具/清除本地存储目录")]
    public static void RemoveLocationRecode()
    {
        InterfacePlatformTools.DeleteJsonFiles();
    }
}
