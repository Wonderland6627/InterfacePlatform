/**
*	Date: #DATE#
*	Description: 预览图信息框
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewInfoPart : MonoBehaviour
{
    [Header("主预览图")]
    public Image mainPreviewImg;
    [Header("小缩略图")]
    public Image[] thumbnailBtns;

    public void Init(ProductInfo info)
    {
        Sprite[] sprites = LibraryManager.GetInstance().GetProductPreviewTextures(info.ProductName);
        mainPreviewImg.SetSprite(sprites[0]);
    }
}
