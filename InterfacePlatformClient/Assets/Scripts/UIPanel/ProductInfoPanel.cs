/**
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
    public Text productName;
    [Header("开发商")]
    public Text companyName;
    [Header("分类")]
    public Text classifyName;

    [Header("收藏")]
    public bool isFavor;

    [Header("预览框")]
    public PreviewInfoPart previewInfoPart;

    [Header("描述")]
    public Text descText;
    [Header("相关内容位置")]
    public Transform relatedProductRoot;

    private int previewIndex = 0;//缩略图索引

    public Button backBtn;

    public override void InitPanel(object param = null)
    {
        base.InitPanel();

        backBtn.onClick.AddListener(OnBackBtnClicked);
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
