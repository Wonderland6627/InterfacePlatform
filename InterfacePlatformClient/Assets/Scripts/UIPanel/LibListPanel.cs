/**
*	Date: #DATE#
*	Description: 库面板信息列表 在这里展示筛选出来的Block
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibListPanel : UIPanel
{
    [Header("Block Root")]
    public Transform blockRoot;

    public List<LibraryInfoUI> libUIinfosList;

    public override void InitPanel(object param = null)
    {
        base.InitPanel();
        LibraryManager.Instance.OnKeywordChangedHandler += OnKeywordChangedHandler;
    }

    /// <summary>
    /// 关键词修改后刷新界面
    /// </summary>
    /// <param name="keywordList"></param>
    private void OnKeywordChangedHandler(List<string> keywordList)
    {
        libUIinfosList.ForEach((item) => item.gameObject.SetActive(true));

        foreach (var item in libUIinfosList)//隐藏关键词不匹配的
        {
            foreach (var key in keywordList)
            {
                if (!item.ProductInfo.Classifies.Contains(key))
                {
                    item.gameObject.SetActive(false);
                }
            }
        }
    }

    public override void ClosePanel()
    {
        LibraryManager.Instance.OnKeywordChangedHandler -= OnKeywordChangedHandler;
        base.ClosePanel();
    }
}
