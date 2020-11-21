/**
*	Date: #DATE#
*	Description: 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 库中软件的信息
/// </summary>
public class ProductInfo
{
    public string productName;//软件名
    public string companyName;//发行商
    public string[] classifies;//标签
    public string description;//描述
    public string[] mediaPaths;//媒体位置
    public string filePath;//文件位置
}

public class LibraryManager : GenericSingleton<LibraryManager> 
{
    public class ProductsDirList
    {
        public List<string> Products;
    }

    public List<string> ProductsList
    {
        get
        {
            return GetProductsInfoList();
        }
    }

    public List<string> GetProductsInfoList()
    {
        string productLists = InterfacePlatformTools.ReadText(JsonType.LibraryProductsInfo);
        if (string.IsNullOrEmpty(productLists))
        {
            Debug.Log("库中无软件");
            return null;
        }

        var productInfosList = InterfacePlatformTools.Deserialize<ProductsDirList>(productLists);
        foreach (var item in productInfosList.Products)
        {
            Debug.Log(item);
        }

        return productInfosList.Products;
    }
}
