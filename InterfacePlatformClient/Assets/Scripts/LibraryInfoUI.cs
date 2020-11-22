using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Product Block产品展示块
/// </summary>
public class LibraryInfoUI : MonoBehaviour, IPointerClickHandler
{
    public Image previewImg;

    public Text productName;
    public Text companyName;
    public Text classify;

    public Button favorBtn;

    private Transform productInfoRoot;

    private ProductInfo info;
    public ProductInfo ProductInfo
    {
        get
        {
            return info;
        }
    }

    public void Init(ProductInfo info, Transform infoRoot)
    {
        Sprite[] sprites = LibraryManager.GetInstance().GetProductPreviewTextures(info.ProductName);
        if(sprites != null && sprites.Length>0)
        {
            previewImg.SetSprite(sprites[0]);
        }

        this.info = info;

        productName.SetText(info.ProductName);
        companyName.SetText(info.CompanyName);
        classify.SetText(info.ClassifiesToString());

        productInfoRoot = infoRoot;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.GetInstance().OpenPanel<ProductInfoPanel>(IPResDictionary.ProductInfoPanel, info, productInfoRoot);
    }
}
