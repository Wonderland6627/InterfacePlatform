using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIWindow : MonoBehaviour
{
    [Header("主页Tog")]
    public OptionTogButton mainTogBtn;

    [Header("库Tog")]
    public OptionTogButton libraryTogBtn;

    [Header("引擎Tog")]
    public OptionTogButton engineTogBtn;

    [Header("驱动Tog")]
    public OptionTogButton driveTogBtn;

    [Header("下载Tog")]
    public OptionTogButton downLoadTogBtn;

    [Header("设置Tog")]
    public OptionTogButton settingTogBtn;

    [Header("ToggleGroup")]
    public ToggleGroup mainTogGroup;

    [Header("主页")]
    public MainPanel mainPanel;

    [Header("库面板")]
    public LibraryPanel libraryPanel;

    [Header("引擎面板")]
    public EnginePanel enginePanel;

    [Header("驱动面板")]
    public DrivePanel drivePanel;

    [Header("下载面板")]
    public DownloadPanel downloadPanel;

    public void Init()
    {
        InitWindow();
    }

    public void InitWindow()
    {
        mainTogBtn.Init(mainTogGroup, mainPanel.gameObject);
        libraryTogBtn.Init(mainTogGroup, libraryPanel.gameObject);
        engineTogBtn.Init(mainTogGroup, enginePanel.gameObject);
        driveTogBtn.Init(mainTogGroup, drivePanel.gameObject);
        downLoadTogBtn.Init(mainTogGroup, downloadPanel.gameObject);
        settingTogBtn.Init(mainTogGroup);

        mainPanel.InitPanel();
        libraryPanel.InitPanel();
        enginePanel.InitPanel();
        drivePanel.InitPanel();
        downloadPanel.InitPanel();

        mainTogBtn.Select();
    }
}
