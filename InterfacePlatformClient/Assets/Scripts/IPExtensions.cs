using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Extensions
{
    public static void SetText(this Text text, string str)
    {
        if (text)
        {
            text.text = str;
        }
    }

    public static void SetSprite(this Image image, Sprite sprite)
    {
        if (image)
        {
            image.sprite = sprite; ;
        }
    }

    public static void SetSprite(this RawImage image, Texture texture)
    {
        if (image)
        {
            image.texture = texture;
        }
    }
}

public static class IPExtensions
{
    public static string ClassifiesToString(this ProductInfo info)
    {
        var classifies = info.Classifies;
        string value = "";
        for (int i = 0; i < classifies.Count; i++)
        {
            value += classifies[i] + ",";
        }

        value = value.Substring(0, value.Length - 1);

        return value;
    }

    public static string ProductInfoStr(this ProductInfo info)
    {
        return JsonUtility.ToJson(info);
    }
}
