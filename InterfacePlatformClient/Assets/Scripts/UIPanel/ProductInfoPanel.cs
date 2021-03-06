﻿/**
*	Date: #DATE#
*	Description: 软件信息面板
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductInfoPanel : UIPanel
{
    [Header("名称")]
    public Text productNameText;
    [Header("开发商")]
    public Text companyNameText;
    [Header("分类")]
    public Text classifyText;
    [Header("收藏")]
    public bool isFavor;
    [Header("开始体验")]
    public ImageButton playBtn;
    [Header("开始下载")]
    public ImageButton downloadBtn;
    [Header("预览框")]
    public PreviewInfoPart previewInfoPart;
    [Header("描述")]
    public Text descText;
    [Header("相关内容位置")]
    public Transform relatedProductRoot;
    [Header("详细信息面板位置")]
    public Transform productInfoRoot;

    public ImageButton backBtn;

    private ProductInfo info;

    public override void InitPanel(object param = null)
    {
        base.InitPanel();
        if (param != null)
        {
            ProductInfo info = param as ProductInfo;
            if (info != null)
            {
                this.info = info;
            }
            else
            {
                Debug.Log("Info is null");
                return;
            }
        }

        productNameText.SetText(info.ProductName);
        companyNameText.SetText(info.CompanyName);
        classifyText.SetText(info.ClassifiesToString());
        descText.SetText(info.Description);

        previewInfoPart.Init(info);
        FindRelatedProducts();

        playBtn.AddListener(OnPlayBtnClicked);
        downloadBtn.AddListener(OnDownloadBtnClicked);
        backBtn.AddListener(OnBackBtnClicked);

        if(string.IsNullOrEmpty(info.FilePath))
        {
            downloadBtn.gameObject.SetActive(true);

            return;
        }
        downloadBtn.gameObject.SetActive(!info.FilePath.EndsWith(".exe"));       
    }

    private void OnPlayBtnClicked()
    {
        if(string.IsNullOrEmpty(info.FilePath) || !info.FilePath.EndsWith(".exe"))
        {
            return;
        }

        CommonFunction.OpenFile(info.FilePath);
    }

    private void OnDownloadBtnClicked()
    {

    }

    /// <summary>
    /// 获取相关软件
    /// </summary>
    private void FindRelatedProducts()
    {
        var relatedInfoList = LibraryManager.GetInstance().GetRelatedProductsInfoListByKeywords(info.Classifies);
        if (relatedInfoList == null || relatedInfoList.Count == 0)
        {
            return;
        }

        relatedInfoList.Remove(info);//相关软件列表移除正在显示的这个
        foreach (var info in relatedInfoList)
        {
            var infoUI = UIManager.Instance.LoadUI<LibraryInfoUI>(IPResDictionary.LibraryInfoUI);
            infoUI.transform.SetParent(relatedProductRoot);
            infoUI.Init(info, transform.parent);
        }
    }

    public void OnBackBtnClicked()
    {
        ClosePanel();
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
    }
}
