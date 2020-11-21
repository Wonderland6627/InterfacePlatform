/**
*	Date: #DATE#
*	Description: 分类选项按钮 可记录多选
*	这个按钮起名注意规范 => 分类名+TogButton
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassifyTogButton : TogButton 
{
    [Space(10)]
    [Header("分类标签")]
    public string classifyTag;//分类标签

    public override void Init(object param = null)
    {
        base.Init(param);
        if(string.IsNullOrEmpty(classifyTag))
        {
            string togName = gameObject.name;
            classifyTag = togName.Remove(togName.LastIndexOf("TogButton"));
        }
    }

    public override void OnApplySelected()
    {
        base.OnApplySelected();
        Debug.Log(toggle.isOn + " " + classifyTag);
    }
}
