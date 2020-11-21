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

    }

    public MessageParam(string _title, string _content)
    {
        this.title = _title;
        this.content = _content;
    }
}

public class MessagePanel : UIPanel
{
    public override void InitPanel(object param = null)
    {
        base.InitPanel();
    }
}
