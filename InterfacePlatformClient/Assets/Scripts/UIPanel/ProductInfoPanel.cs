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
    public Text productNameText;
    [Header("开发商")]
    public Text companyNameText;
    [Header("分类")]
    public Text classifyText;

    [Header("收藏")]
    public bool isFavor;

    [Header("预览框")]
    public PreviewInfoPart previewInfoPart;

    [Header("描述")]
    public Text descText;
    [Header("相关内容位置")]
    public Transform relatedProductRoot;

    public ImageButton backBtn;

    private ProductInfo info;

    public override void InitPanel(object param = null)
    {
        base.InitPanel();
        if(param != null)
        {
            ProductInfo info = param as ProductInfo;
            Debug.Log("实际" + info.ProductInfoStr());
            if(info!=null)
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

        backBtn.AddListener(OnBackBtnClicked);
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
