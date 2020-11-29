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

    public List<UIPanel> UIPanelsList;

    [Header("顶层UI")]
    public Transform TopUIRoot;
    [Header("主面板的右侧位置")]
    public Transform UIPanelRoot;

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

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        UIPanelsList = new List<UIPanel>();
        for (int i = 0; i < UIPanelRoot.childCount; i++)
        {
            UIPanelsList.Add(UIPanelRoot.GetChild(i).GetComponent<UIPanel>());
        }
    }

    //指定parent
    public T OpenPanel<T>(string path, object openParam = null, Transform parent = null) where T : UIPanel
    {
        T panel = Resources.Load<T>(path) as T;
        if (panel != null)
        {
            panel = Instantiate(panel, parent == null ? UIPanelRoot : parent);
            panel.InitPanel(openParam);
            UIPanelsList.Add(panel);
        }

        return panel;
    }

    public T LoadUI<T>(string path) where T : UnityEngine.Object
    {
        T instance = null;
        T loadRes = Resources.Load<T>(path);
        if (loadRes != null)
        {
            instance = Instantiate(loadRes);
        }

        return instance;
    }

    public void RemovePanel(UIPanel panel)
    {
        UIPanelsList.Remove(panel);
    }

    /// <summary>
    /// 获取右侧最上层面板
    /// </summary>
    /// <returns></returns>
    public UIPanel GetTopRightUIPanel()
    {
        if (UIPanelRoot.childCount == 0)
        {
            Debug.Log("无TopPanel");

            return null;
        }

        return UIPanelRoot.GetChild(UIPanelRoot.childCount - 1).GetComponent<UIPanel>();
    }

    /// <summary>
    /// 获取最上层面板
    /// </summary>
    /// <returns></returns>
    public UIPanel GetTopUIPanel()
    {
        if (TopUIRoot.childCount == 0)
        {
            Debug.Log("无TopPanel");

            return null;
        }

        return TopUIRoot.GetChild(TopUIRoot.childCount - 1).GetComponent<UIPanel>();
    }

    /// <summary>
    /// 提示弹窗
    /// </summary>
    public void ShowMessagePanel(MessageParam param)
    {
        MessagePanel msgPanelRes = LoadUI<MessagePanel>(IPResDictionary.MessagePanel);
        if (msgPanelRes == null)
        {
            Debug.LogError("提示窗加载失败");
            return;
        }

        MessagePanel msgPanel = Instantiate(msgPanelRes, TopUIRoot) as MessagePanel;
        msgPanel.InitPanel(param);
        UIPanelsList.Add(msgPanel);
    }
}
