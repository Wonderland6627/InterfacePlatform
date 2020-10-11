using System.Collections;
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

    [Header("引擎面板")]
    public EnginePanel enginePanel;

    private void Start()
    {
        InitWindow();
    }

    public void InitWindow()
    {
        mainTogBtn.Init(mainTogGroup, mainPanel.gameObject);
        libraryTogBtn.Init(mainTogGroup);
        engineTogBtn.Init(mainTogGroup, enginePanel.gameObject);
        downLoadTogBtn.Init(mainTogGroup);
        settingTog.Init(mainTogGroup);

        mainPanel.InitPanel();
        mainPanel.gameObject.SetActive(false);

        enginePanel.InitPanel();
        enginePanel.gameObject.SetActive(false);

        mainTogBtn.Select();
    }

    /// <summary>
    /// 点击引擎面板
    /// </summary>
    private void OnEngineTogBtnClick()
    {

    }
}
