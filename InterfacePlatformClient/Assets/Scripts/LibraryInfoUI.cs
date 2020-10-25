using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public enum LibraryType
{
    PC,
    VR,
    LeapMotion,
    Kinect,
}

public class LibraryInfoUI : UIBlockBase 
{
    public override void Init(Enum type, EXEFileLocation location)
    {
        base.Init(type, location);

        LibraryType libraryType = (LibraryType)type;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (!string.IsNullOrEmpty(fileLocation.localpath))
        {
            CommonFunction.OpenFile(fileLocation.localpath);
        }
    }
}
