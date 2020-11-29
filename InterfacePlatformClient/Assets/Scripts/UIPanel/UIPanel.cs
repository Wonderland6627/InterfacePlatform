using UnityEngine;

public class UIPanel : MonoBehaviour
{
    [Header("面板描述")]
    [TextArea()]
    public string desc;
    [Space(10)]

    private bool isResume = false;//需要被刷新

    public virtual void InitPanel(object param = null) { }

    protected virtual void OnEnable()
    {
        if (isResume)
        {
            OnResume();
            isResume = false;
        }
    }

    public virtual void OnResume() { }

    public virtual void HidePanel()
    {
        isResume = true;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 记得处理事件写在base()之前
    /// </summary>
    public virtual void ClosePanel()
    {
        UIManager.GetInstance().RemovePanel(this);
        Destroy(gameObject);
    }
}