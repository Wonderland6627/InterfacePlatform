﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIWindow : MonoBehaviour
{
    [Header("主页Tog")]
    public TogButton mainTogBtn;

    [Header("库Tog")]
    public TogButton libraryTogBtn;

    [Header("引擎Tog")]
    public TogButton engineTogBtn;

    [Header("下载Tog")]
    public TogButton downLoadTogBtn;

    [Header("设置Tog")]
    public TogButton settingTog;

    [Header("ToggleGroup")]
    public ToggleGroup mainTogGroup;

    [Header("主页")]
    public MainPanel mainPanel;

    [Header("库面板")]
    public LibraryPanel libraryPanel;

    [Header("引擎面板")]
    public EnginePanel enginePanel;

    [Header("下载面板")]
    public DownloadPanel downloadPanel;

    private void Start()
    {
        InitWindow();
    }

    public void InitWindow()
    {
        mainTogBtn.Init(mainTogGroup, mainPanel.gameObject);
        libraryTogBtn.Init(mainTogGroup, libraryPanel.gameObject);
        engineTogBtn.Init(mainTogGroup, enginePanel.gameObject);
        downLoadTogBtn.Init(mainTogGroup, downloadPanel.gameObject);
        settingTog.Init(mainTogGroup);

        mainPanel.InitPanel();
        libraryPanel.InitPanel();
        enginePanel.InitPanel();
        downloadPanel.InitPanel();

        mainTogBtn.Select();
    }
}
