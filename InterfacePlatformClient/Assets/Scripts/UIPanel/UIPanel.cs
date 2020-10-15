using UnityEngine;

public class UIPanel : MonoBehaviour
{
    [Header("面板描述")]
    [TextArea()]
    public string desc;
    [Space(10)]

    private bool isResume = false;//需要被刷新

    public virtual void InitPanel() { }

    protected virtual void OnEnable()
    {
        if (isResume)
        {
            OnResume();
            isResume = false;
        }
    }

    public virtual void OnResume() { }

    public virtual void ClosePanel()
    {
        isResume = true;
    }
}