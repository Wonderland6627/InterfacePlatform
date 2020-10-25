using System;
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
public class EngineInfoUI : UIBlockBase
{
    public Image unityIconImg;
    public Image unrealIconImg;
    public Text versionText;

    [Header("移除此版本")]
    public Button removeVersionBtn;

    public override void Init(Enum type, EXEFileLocation location)
    {
        base.Init(type, location);

        SetAllActiveFalse();

        EngineType engineType = (EngineType)type;
        if (engineType == EngineType.UnityEngine)
        {
            unityIconImg.gameObject.SetActive(true);
        }
        else if (engineType == EngineType.UnrealEngine)
        {
            unrealIconImg.gameObject.SetActive(true);
        }

        fileLocation = location;
        versionText.text = location.version;
    }

    private void SetAllActiveFalse()
    {
        unityIconImg.gameObject.SetActive(false);
        unrealIconImg.gameObject.SetActive(false);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if(!string.IsNullOrEmpty(fileLocation.localpath))
        {
            CommonFunction.OpenFile(fileLocation.localpath);
        }
    }
}
