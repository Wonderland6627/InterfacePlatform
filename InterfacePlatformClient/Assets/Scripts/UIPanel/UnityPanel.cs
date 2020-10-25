using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

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

    [Header("选项卡Root")]
    public Transform scrollContent;

    [Header("引擎选项卡预制体")]
    public EngineInfoUI engineUIPrefab;

    public string installerPath;
    [SerializeField]
    private string selectedVersion;

    /// <summary>
    /// Unity Editor版本和安装包下载地址
    /// </summary>
    private Dictionary<string, string> versionDownloadTable;//下载地址，检测本地没有安装包后使用
    private List<EXEFileLocation> versionLocationsList;//编辑器地址，存储Editor路径

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
        List<Dropdown.OptionData> optionsList = new List<Dropdown.OptionData>();
        foreach (var pairs in versionDownloadTable)//根据表初始化下拉框内容
        {
            optionsList.Add(new Dropdown.OptionData(pairs.Key));
        }
        versionDropdown.AddOptions(optionsList);

        unityBtn.onClick.AddListener(OpenUnityCN);
        addVersionBtn.onClick.AddListener(OnAddVersionBtnClick);
        installUnityBtn.onClick.AddListener(InstallUnity);
        versionDropdown.onValueChanged.AddListener(OnSelectVersionChanged);

        selectedVersion = versionDropdown.captionText.text;

        InitAllUnityLocalVersion();
    }

    /// <summary>
    /// 初始化所有本地有的版本UnityEditor
    /// StreamingAsset下面存一个模板
    /// </summary>
    private void InitAllUnityLocalVersion()
    {
        versionLocationsList = new List<EXEFileLocation>();

        string localEditorInfo = InterfacePlatformTools.ReadText(JsonType.UnityEditorLocalVersion);
        if (string.IsNullOrEmpty(localEditorInfo))
        {
            return;
        }

        List<EXEFileLocation> exeLocations = InterfacePlatformTools.Deserialize<List<EXEFileLocation>>(localEditorInfo);
        foreach (var item in exeLocations)
        {
            var location = new EXEFileLocation(item.version, item.localpath);
            versionLocationsList.Add(location);//存入字典 方便添加时覆盖
            EngineInfoUI engineUI = Instantiate(engineUIPrefab, scrollContent) as EngineInfoUI;
            engineUI.Init(EngineType.UnityEngine, location);
        }
    }

    private void OnAddVersionBtnClick()
    {
        ForeachUnityEditor();
    }

    private void ForeachUnityEditor()
    {
        CommonFunction.OpenDirectory("exe", OnForeachSuccess, OnForeachFailed);
    }

    private void OnForeachSuccess(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            Debug.LogError("Editor选择路径为空");
            return;
        }
        if (!filePath.EndsWith(@"Editor\Unity.exe"))
        {
            Debug.LogError("选择的不是Unity.exe");
            return;
        }

        FileVersionInfo fileVerInfo = FileVersionInfo.GetVersionInfo(filePath);//获取exe版本信息
        string version = string.Format("{0}.{1}.{2}", fileVerInfo.FileMajorPart, fileVerInfo.FileMinorPart, fileVerInfo.FileBuildPart);
        AddAndSaveLocalVersion(new EXEFileLocation(version, filePath));
    }

    /// <summary>
    /// 添加本地已有版本
    /// </summary>
    private void AddAndSaveLocalVersion(EXEFileLocation location)
    {
        if (versionLocationsList.Exists((item) => { return item.version == location.version; }))//传入的location和list中已有的只是值相同 还是要通过version比较一下
        {
            Debug.LogError(location.version + "已添加");
            return;
        }

        EngineInfoUI engineUI = Instantiate(engineUIPrefab, scrollContent) as EngineInfoUI;
        engineUI.Init(EngineType.UnityEngine, location);
        versionLocationsList.Add(location);

        string json = InterfacePlatformTools.Serialize(versionLocationsList);
        InterfacePlatformTools.WriteBytes(JsonType.UnityEditorLocalVersion, json);
    }

    private void RemoveLocalVersion()
    {

    }

    private void OnForeachFailed()
    {
        //todo 弹出提示框
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
        string installerPath = GetInstallerPath();
        if (string.IsNullOrEmpty(installerPath))
        {
            Debug.LogError("安装包路径为空");
            return;
        }
        if (!File.Exists(installerPath))
        {
            Debug.Log("安装包不存在");
            string url = "";
            if (versionDownloadTable.TryGetValue(selectedVersion, out url))
            {
                Application.OpenURL(url);
                Debug.Log("打开下载页");
            }
            else
            {
                OpenUnityCN();
                Debug.LogError("链接有误，前往官网");
            }
            return;
        }

        CommonFunction.OpenFile(installerPath);
    }

    private string GetInstallerPath()
    {
        installerPath = string.Format("{0}/{1}{2}{3}", Application.streamingAssetsPath, Prefix, selectedVersion, Suffix);
        return installerPath;
    }
}
