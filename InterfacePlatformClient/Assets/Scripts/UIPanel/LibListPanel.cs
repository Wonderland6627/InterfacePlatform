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

    public override void InitPanel(object param = null)
    {
        base.InitPanel();
    }
}
