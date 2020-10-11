using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnginePanel : UIPanel
{
    [Header("UnityTog")]
    public TogButton unityTogBtn;

    [Header("UnrealTog")]
    public TogButton unrealTogBtn;

    public ToggleGroup titleTogGroup;

    [Header("Unity面板")]
    public UnityPanel unityPanel;

    public override void InitPanel()
    {
        base.InitPanel();

        unityTogBtn.Init(titleTogGroup);
        unrealTogBtn.Init(titleTogGroup);

        unityPanel.InitPanel();

        unityTogBtn.Select();
    }
}
