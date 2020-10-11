using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum EngineType
{
    UnityEngine,
    UnrealEngine,
}

/// <summary>
/// 安装页面引擎选项卡，显示、记录各版本引擎本地路径
/// </summary>
public class EngineInfoUI : MonoBehaviour, IPointerClickHandler
{
    public string localPath;

    public Image unityIconImg;
    public Image unrealIconImg;
    public Text versionText;

    [Header("移除此版本")]
    public Button removeVersionBtn;

    private void Start()
    {
        unityIconImg.gameObject.SetActive(false);
        unrealIconImg.gameObject.SetActive(false);
    }

    public void Init(EngineType type, string version)
    {
        if (type == EngineType.UnityEngine)
        {
            unityIconImg.gameObject.SetActive(true);
        }
        else if (type == EngineType.UnrealEngine)
        {
            unrealIconImg.gameObject.SetActive(true);
        }
        versionText.text = version;
    }

    /// <summary>
    /// 移除此版本
    /// </summary>
    private void RemoveVersion()
    {
        string version = versionText.text;
        if(string.IsNullOrEmpty(version))
        {
            Debug.LogError("版本为空");
            return;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!string.IsNullOrEmpty(localPath))
        {
            CommonFunction.OpenFile(localPath);
        }
    }
}
