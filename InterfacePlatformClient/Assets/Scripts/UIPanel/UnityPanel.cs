using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityPanel : UIPanel
{
    [Header("Unity官网")]
    public Button unityBtn;

    [Header("添加Unity版本")]
    public Button addVersionBtn;

    [Header("安装Unity")]
    public Button installUnityBtn;

    [Header("版本选择")]
    public Dropdown versionDropdown;

    [Header("引擎选项卡预制体")]
    public EngineInfoUI engineUIPrefab;

    public string installerPath;
    [SerializeField]
    private string selectedVersion;
    private Dictionary<string, string> versionDownloadTable;//下载地址，检测本地没有安装包后使用

    private const string Prefix = "UnityDownloadAssistant-";
    private const string Suffix = ".exe";

    public override void InitPanel()
    {
        base.InitPanel();

        versionDownloadTable = new Dictionary<string, string>()
        {
            { "2017.4.24f1", "https://download.unitychina.cn/download_unity/786769fc3439/UnityDownloadAssistant-2017.4.24f1.exe" },
            { "2018.2.17f1", "https://download.unitychina.cn/download_unity/88933597c842/UnityDownloadAssistant-2018.2.17f1.exe" },
            { "2019.4.0f1", "https://download.unitychina.cn/download_unity/38dbd65869c4/Windows64EditorInstaller/UnitySetup64.exe" },
        };

        unityBtn.onClick.AddListener(OpenUnityCN);
        addVersionBtn.onClick.AddListener(OnAddVersionBtnClick);
        installUnityBtn.onClick.AddListener(InstallUnity);
        versionDropdown.onValueChanged.AddListener(OnSelectVersionChanged);

        selectedVersion = versionDropdown.captionText.text;
    }

    private void OnAddVersionBtnClick()
    {
        CommonFunction.OpenDirectory("exe");
    }

    /// <summary>
    /// 打开Unity官网
    /// </summary>
    private void OpenUnityCN()
    {
        string url = "https://unity.cn/";
        Application.OpenURL(url);
    }

    /// <summary>
    /// 下拉框值变化的时候更改选择的安装包路径
    /// </summary>
    private void OnSelectVersionChanged(int value)
    {
        versionDropdown.value = value;
        selectedVersion = versionDropdown.captionText.text;
    }

    private void InstallUnity()
    {
        if (string.IsNullOrEmpty(GetInstallerPath()))
        {
            Debug.LogError("安装包路径为空");
            return;
        }
        if(!File.Exists(GetInstallerPath()))
        {
            Debug.Log("安装包不存在");
            string url = "";
            if (versionDownloadTable.TryGetValue(selectedVersion,out url))
            {
                Application.OpenURL(url);
                Debug.Log("打开下载页");
            }
            else
            {
                OpenUnityCN();
                Debug.LogError("链接有误，前往官网");       
            }
        }
        CommonFunction.OpenFile(GetInstallerPath());
    }

    private string GetInstallerPath()
    {
        installerPath = string.Format("{0}/{1}{2}{3}", Application.streamingAssetsPath, Prefix, selectedVersion, Suffix);
        return installerPath;
    }
}
