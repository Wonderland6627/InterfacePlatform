using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            return _instance;
        }

        set
        {
            _instance = value;
        }
    }

    public static UIManager GetInstance()
    {
        if (_instance != null)
        {
            return _instance;
        }

        return null;
    }

    public List<UIPanel> uiPanelsList;

    [Header("顶层UI")]
    public Transform topUIRoot;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            if (Application.isPlaying)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    private void Init()
    {
        uiPanelsList = new List<UIPanel>();
    }

    /// <summary>
    /// 提示弹窗
    /// </summary>
    private void ShowMessagePanel(MessageParam param)
    {
        MessagePanel messagePanel = Resources.Load<MessagePanel>("UIPanel/MessagePanel");
        if(messagePanel == null)
        {
            Debug.LogError("提示窗加载失败");
            return;
        }

        messagePanel.InitPanel(param);
    }
}
