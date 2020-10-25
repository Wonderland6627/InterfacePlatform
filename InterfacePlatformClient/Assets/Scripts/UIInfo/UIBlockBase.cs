using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// UI信息显示基类
/// </summary>
public class UIBlockBase : MonoBehaviour, IPointerClickHandler
{
    public EXEFileLocation fileLocation;

    public virtual void Init(Enum type, EXEFileLocation location)
    {

    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
