using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class LibraryTools
{
    public static void CreateProductInfoDir
        (string productName,
         string companyName,
         string classifies,
         string description,
         string filePath)
    {
        InterfacePlatformTools.CreateJsonFiles();

        string dirRootPath = LibraryManager.GetProductRootDirPath();
        if (!Directory.Exists(dirRootPath))
        {
            Directory.CreateDirectory(dirRootPath);
        }
        string productDirPath = dirRootPath + productName;
        string previewDirPath = productDirPath + "/Preview";
        Directory.CreateDirectory(productDirPath);
        Directory.CreateDirectory(previewDirPath);

        var classifiesList = classifies.Split(',').ToList();
        if (classifiesList == null)
        {
            Debug.Log("标签必须使用‘,’进行分段");
            return;
        }

        ProductInfo productInfo = new ProductInfo
        {
            ProductName = productName,
            CompanyName = companyName,
            Classifies = classifiesList,
            Description = description,
            FilePath = filePath,
        };

        string json = InterfacePlatformTools.Serialize(productInfo);
        string jsonPath = productDirPath + "/ProductInfo.json";
        File.WriteAllText(jsonPath, json, System.Text.Encoding.UTF8);
        Debug.Log("创建完成");

        string productDirListstr = InterfacePlatformTools.ReadText(JsonType.LibraryProductsList);
        LibraryManager.ProductsDirList productsDirList = new LibraryManager.ProductsDirList();
        if (!string.IsNullOrEmpty(productDirListstr))//如果之前有内容
        {
            productsDirList = InterfacePlatformTools.Deserialize<LibraryManager.ProductsDirList>(productDirListstr);
        }
        if (!productsDirList.Products.Contains(productName))
        {
            productsDirList.Products.Add(productName);
        }

        string newProductDirListstr = InterfacePlatformTools.Serialize(productsDirList);
        File.WriteAllText(InterfacePlatformTools.GetJsonFilePath(JsonType.LibraryProductsList), newProductDirListstr, System.Text.Encoding.UTF8);
    }
}

public class LibraryToolsWindow : EditorWindow
{
    public string ProductName;      //软件名
    public string CompanyName;      //发行商
    public string Classifies;       //标签
    public string Description;      //描述
    public string FilePath;         //文件位置

    [MenuItem("开发工具/LibraryTools", priority = 1)]
    public static void OpenWindow()
    {
        EditorWindow.GetWindow<LibraryToolsWindow>();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("软件名");
        ProductName = EditorGUILayout.TextField(ProductName);

        EditorGUILayout.LabelField("发行商");
        CompanyName = EditorGUILayout.TextField(CompanyName);

        EditorGUILayout.LabelField("标签(用英文,进行分隔)");
        Classifies = EditorGUILayout.TextField(Classifies);

        EditorGUILayout.LabelField(@"描述(注意换行的\n)");
        Description = EditorGUILayout.TextField(Description);

        EditorGUILayout.LabelField("文件位置");
        FilePath = EditorGUILayout.TextField(FilePath);

        GUILayout.Space(10);
        if (GUILayout.Button("创建软件本地文件夹"))
        {
            LibraryTools.CreateProductInfoDir
               (ProductName,
                CompanyName,
                Classifies,
                Description,
                FilePath);
        }
    }
}