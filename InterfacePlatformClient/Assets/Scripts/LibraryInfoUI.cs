using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProductInfoOpenParam
{
    public string productName;
}

/// <summary>
/// Product Block产品展示块
/// </summary>
public class LibraryInfoUI : MonoBehaviour, IPointerClickHandler
{
    public Text productName;
    public Text companyName;
    public Text classify;

    public Button favorBtn;

    public Transform productInfoRoot;

    public void Init(Transform infoRoot)
    {
        productInfoRoot = infoRoot;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.GetInstance().OpenPanel<ProductInfoPanel>(IPResDictionary.ProductInfoPanel,
            new ProductInfoOpenParam() { productName = this.productName.text },
            productInfoRoot);
    }
}
