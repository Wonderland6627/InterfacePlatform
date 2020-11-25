/**
*	Date: #DATE#
*	Description: Library库面板重构，增加检索、分类
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LibraryPanel : UIPanel
{
    public ProductInfo inf;
    [Header("设备分类选钮组")]
    public ClassifyTogButton[] hardwareTogBtns;
    [Header("内容分类选钮组")]
    public ClassifyTogButton[] contentTogBtns;
    [Header("平台分类选钮组")]
    public ClassifyTogButton[] platformTogBtns;

    private List<ClassifyTogButton> allTogBtnsList;

    [Header("搜索框")]
    public InputField searchInput;
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
        libListPanel.InitPanel();

        InitAllLibraryInfoUIs();

        searchInput.onValueChanged.AddListener(OnSearchInputValueChanged);
    }

    private void InitAllLibraryInfoUIs()
    {
        var productList = LibraryManager.Instance.ProductsList;//产品名列表
        if (productList == null || productList.Count == 0)
        {
            return;
        }

        foreach (var productName in productList)
        {
            var infoUI = UIManager.Instance.LoadUI<LibraryInfoUI>(IPResDictionary.LibraryInfoUI);
            infoUI.transform.SetParent(libListPanel.blockRoot);
            libListPanel.libUIinfosList.Add(infoUI);

            ProductInfo info = LibraryManager.GetInstance().GetProductInfoByProductName(productName);
            infoUI.Init(info, productInfoRoot);//传入详细页面的父物体，所有详细页面生成在这下面
        }
    }

    private void OnSearchInputValueChanged(string value)
    {
        
    }
}
