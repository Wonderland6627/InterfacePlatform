using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageParam
{
    public string title;
    public string content;

    public MessageParam()
    {
        this.title = "系统提示";
    }

    public MessageParam(string _title, string _content)
    {
        this.title = _title;
        this.content = _content;
    }
}

public class MessagePanel : UIPanel
{
    [Header("标题")]
    public Text titleText;
    [Header("内容")]
    public Text contentText;

    private CanvasGroup canvasGroup;

    public override void InitPanel(object param = null)
    {     
        if(param == null)
        {
            return;
        }

        base.InitPanel();

        MessageParam message = param as MessageParam;
        if(message == null)
        {
            return;
        }

        canvasGroup = GetComponent<CanvasGroup>();
        if(canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent(typeof(CanvasGroup)) as CanvasGroup;
        }

        titleText.SetText(message.title);
        contentText.SetText(message.content);

        StartCoroutine(ClosePanelCor());
    }

    private IEnumerator ClosePanelCor()
    {
        var frame = new WaitForEndOfFrame();
        for (float f = 1; f > 0 ; f-=0.005f)
        {
            yield return frame;
            canvasGroup.alpha = f;
        }
        ClosePanel();
    }

    public override void ClosePanel()
    {
        StopAllCoroutines();

        base.ClosePanel();
    }
}
