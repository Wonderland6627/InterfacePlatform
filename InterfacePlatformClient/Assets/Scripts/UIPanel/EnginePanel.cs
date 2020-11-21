using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnginePanel : UIPanel
{
    [Header("UnityTog")]
    public OptionTogButton unityTogBtn;

    [Header("UnrealTog")]
    public OptionTogButton unrealTogBtn;

    public ToggleGroup titleTogGroup;

    [Header("Unity面板")]
    public UnityPanel unityPanel;

    public override void InitPanel(object param = null)
    {
        base.InitPanel();

        unityTogBtn.Init(titleTogGroup, unityPanel.gameObject);
        unrealTogBtn.Init(titleTogGroup);

        unityPanel.InitPanel();
        unityPanel.gameObject.SetActive(false);

        unityTogBtn.Select();
    }
}
