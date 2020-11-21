/**
*	Date: #DATE#
*	Description: Library库面板重构，增加检索、分类
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LibraryPanel : UIPanel
{
    [Header("设备分类选钮组")]
    public ClassifyTogButton[] hardwareTogBtns;
    [Header("内容分类选钮组")]
    public ClassifyTogButton[] contentTogBtns;
    [Header("平台分类选钮组")]
    public ClassifyTogButton[] platformTogBtns;

    public List<ClassifyTogButton> allTogBtnsList;

    [Header("列表面板")]
    public LibListPanel libListPanel;
    [Header("详细信息面板位置")]
    public Transform productInfoRoot;

    public ToggleGroup titleTogGroup;

    public override void InitPanel(object param = null)
    {
        base.InitPanel();
        allTogBtnsList = hardwareTogBtns.Concat(contentTogBtns).Concat(platformTogBtns).ToList();
        allTogBtnsList.ForEach((togBtn) => { togBtn.Init(); });
        Debug.Log(allTogBtnsList.Count);
        InitAllLibraryInfoUIs();
    }

    private void InitAllLibraryInfoUIs()
    {
        var infoList = LibraryManager.Instance.ProductsList;
        for (int i = 0; i < infoList.Count; i++)
        {
            UIManager.Instance.LoadUI<LibraryInfoUI>(IPResDictionary.LibraryInfoUI);
        }
    }
}
