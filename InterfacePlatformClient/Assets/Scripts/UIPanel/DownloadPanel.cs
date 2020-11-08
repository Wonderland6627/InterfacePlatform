using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownloadPanel : UIPanel
{
    [Header("关闭按钮")]
    public Button closeBtn;

    [Header("下载进度条")]
    public DownloadProgressBar downLoadPrgressBarPrefab;

    public override void InitPanel()
    {
        base.InitPanel();

        closeBtn.onClick.AddListener(ClosePanel);
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
    }
}
