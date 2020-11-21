/**
*	Date: #DATE#
*	Description: 选项按钮，用于控制物体
*/

using UnityEngine;
using UnityEngine.UI;

public class OptionTogButton : TogButton
{
    public override void Init(ToggleGroup group, object param = null)
    {
        base.Init(group, param);
        if (param != null)
        {
            GameObject go = param as GameObject;
            if (go == null)
            {
                go = new GameObject(gameObject.name + "Temp");
                Debug.Log("创建Temp物体 " + go.name);
            }
            inControlGo = go;
        }
    }

    public override void OnApplySelected()
    {
        base.OnApplySelected();
        if (inControlGo)
        {
            inControlGo.SetActive(IsSelect);
        }
    }
}
