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
    [Header("小缩略图Root")]
    public Transform thumbnailsRoot;

    private ProductInfo info;

    public void Init(ProductInfo info)
    {
        if(info == null)
        {
            Debug.Log("Info is null");

            return;
        }

        this.info = info;
        InitThumbnailImages();
    }

    /// <summary>
    /// 生成预览图
    /// </summary>
    private void InitThumbnailImages()
    {
        Sprite[] sprites = LibraryManager.GetInstance().GetProductPreviewTextures(info.ProductName);
        if (sprites != null && sprites.Length > 0)
        {
            mainPreviewImg.SetSprite(sprites[0]);
            
            for (int i = 0; i < sprites.Length; i++)
            {
                ImageButton imgBtn = UIManager.GetInstance().LoadUI<ImageButton>(IPResDictionary.ThumbnailImage);
                imgBtn.image.sprite = sprites[i];
                imgBtn.transform.SetParent(thumbnailsRoot);
                imgBtn.AddListener(() =>
                {
                    mainPreviewImg.sprite = imgBtn.image.sprite;
                });
            }
        }
    }
}
