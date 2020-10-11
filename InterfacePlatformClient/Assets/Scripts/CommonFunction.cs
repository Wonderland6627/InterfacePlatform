using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

using System.Runtime.InteropServices;
using UnityEngine;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenFileName
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
}

public class LocalDialog
{
    //链接指定系统函数       打开文件对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
    public static bool GetOFN([In, Out] OpenFileName ofn)
    {
        return GetOpenFileName(ofn);//执行打开文件的操作
    }

    //链接指定系统函数        另存为对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetSaveFileName([In, Out] OpenFileName ofn);
    public static bool GetSFN([In, Out] OpenFileName ofn)
    {
        return GetSaveFileName(ofn);//执行保存选中文件的操作
    }
}

public class CommonFunction
{
    public static void OpenFile(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("空路径");
            return;
        }

        if (!File.Exists(path))
        {
            Debug.LogError("未找到文件 " + path);
            return;
        }

        Process process = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo(path);
        process.StartInfo = startInfo;
        process.Start();
        Debug.Log("打开文件 " + path);

        process.Dispose();
    }

    /// <summary>
    /// 打开文件选框 type文件类型 string是文件路径
    /// </summary>
    /// <param name="type"></param>
    public static void OpenDirectory(string type, Action<string> successCallback,Action failedCallback)
    {
        OpenFileName openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);
        openFileName.filter = "文件(*." + type + ")\0*." + type + "";
        openFileName.file = new string(new char[256]);
        openFileName.maxFile = openFileName.file.Length;
        openFileName.fileTitle = new string(new char[64]);
        openFileName.maxFileTitle = openFileName.fileTitle.Length;
        openFileName.initialDir = Application.dataPath.Replace('/', '\\');//默认路径
        openFileName.title = "选择文件";
        openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

        if (LocalDialog.GetOpenFileName(openFileName))//点击系统对话框框保存按钮
        {
            if(successCallback != null)
            {
                successCallback.Invoke(openFileName.file);
            }
        }
        else
        {
            if(failedCallback !=null)
            {
                failedCallback.Invoke();
            }
        }
    }
}
