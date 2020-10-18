using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelEventMgr
{
    public delegate void RemoveExeLocateVersionDelegate(string version);
    public event RemoveExeLocateVersionDelegate RemoveExeLocateVersionHandler;
    public void RemoveExeLocationVersion(string version)
    {
        if(RemoveExeLocateVersionHandler!=null)
        {
            RemoveExeLocateVersionHandler.Invoke(version);
        }
    }
}
