/**
*	Date: #DATE#
*	Description: 
*/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 库中软件的信息
/// </summary>
public class ProductInfo
{
    public string ProductName;      //软件名
    public string CompanyName;      //发行商
    public List<string> Classifies; //标签
    public string Description;      //描述
    public string FilePath;         //文件位置
}

public class LibraryManager : GenericSingleton<LibraryManager>
{
    public List<string> keywordList;//筛选关键词
    public string searchKeyword;//用户输入查找的关键词

    public delegate void OnKeywordChangedDelegate(List<string> keywordList);
    public event OnKeywordChangedDelegate OnKeywordChangedHandler;

    public delegate void OnSearchInputValueChangedDelegate(string search);
    public event OnSearchInputValueChangedDelegate OnSearchInputValueChangedHandler;

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

    public List<ProductInfo> productInfosList;

    public void InitManager()
    {
        keywordList = new List<string>();
        productInfosList = new List<ProductInfo>();
    }

    /// <summary>
    /// 当关键词发生变化，类似修改Define
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="selected"></param>
    public void OnKeywordsChanged(string keyword, bool selected)
    {
        if (selected)
        {
            if (!keywordList.Contains(keyword))
            {
                keywordList.Add(keyword);
            }
        }
        else
        {
            if (keywordList.Contains(keyword))
            {
                keywordList.Remove(keyword);
            }
        }

        if (OnKeywordChangedHandler != null)
        {
            OnKeywordChangedHandler.Invoke(keywordList);
        }

        /*Debug.Log(keywordList.Count);
        keywordList.ForEach((item) => { Debug.Log(item); });*/
    }

    /// <summary>
    /// 产品名字列表
    /// </summary>
    /// <returns></returns>
    public List<string> GetProductsInfoList()
    {
        string productLists = InterfacePlatformTools.ReadText(JsonType.LibraryProductsList);
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

    /// <summary>
    /// 根据产品名获取产品信息
    /// </summary>
    /// <param name="productName"></param>
    /// <returns></returns>
    public ProductInfo GetProductInfoByProductName(string productName)
    {
        string jsonPath = GetProductInfoJsonPath(productName);
        string json = InterfacePlatformTools.ReadText(jsonPath);

        ProductInfo info = InterfacePlatformTools.Deserialize<ProductInfo>(json);
        productInfosList.Add(info);

        return info;
    }

    /// <summary>
    /// 产品文件夹路径
    /// </summary>
    private string GetProductDirPath(string productName)
    {
        return string.Format("{0}/{1}/{2}/", Application.persistentDataPath, "Products", productName);
    }

    private string GetProductInfoJsonPath(string productName)
    {
        return GetProductDirPath(productName) + "ProductInfo.json";
    }

    private string GetProductPreviewDirPath(string productName)
    {
        return GetProductDirPath(productName) + "Preview/";
    }

    public Sprite[] GetProductPreviewTextures(string productName)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(GetProductPreviewDirPath(productName));
        FileInfo[] fileInfos = dirInfo.GetFiles();

        List<Sprite> sprites = new List<Sprite>();
        for (int i = 0; i < fileInfos.Length; i++)
        {
            FileStream fs = new FileStream(fileInfos[i].FullName, FileMode.Open);
            fs.Seek(0, SeekOrigin.Begin);
            byte[] bs = new byte[fs.Length];
            fs.Read(bs, 0, bs.Length);
            fs.Close();
            fs.Dispose();
            fs = null;

            Texture2D texture = new Texture2D(500, 300);
            texture.LoadImage(bs);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            sprites.Add(sprite);
        }

        return sprites.ToArray();
    }
}
