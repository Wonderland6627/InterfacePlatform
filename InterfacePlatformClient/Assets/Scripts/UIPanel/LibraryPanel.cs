/**
*	Date: #DATE#
*	Description: 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LibraryPanel : UIPanel
{
    [Header("PCTog")]
    public TogButton pcTogBtn;

    [Header("VRTog")]
    public TogButton vrTogBtn;

    [Header("LeapMotionTog")]
    public TogButton leapmotionTogBtn;

    [Header("KinectTog")]
    public TogButton kinectTogBtn;

    [Header("PC面板")]
    public PCLibPanel pcLibPanel;

    [Header("VR面板")]
    public VRLibPanel vrLibPanel;

    public ToggleGroup titleTogGroup;

    public override void InitPanel()
    {
        base.InitPanel();

        pcTogBtn.Init(titleTogGroup, pcLibPanel.gameObject);
        vrTogBtn.Init(titleTogGroup, vrLibPanel.gameObject);
        leapmotionTogBtn.Init(titleTogGroup);
        kinectTogBtn.Init(titleTogGroup);

        pcLibPanel.InitPanel();
        pcLibPanel.gameObject.SetActive(false);

        vrLibPanel.InitPanel();
        vrLibPanel.gameObject.SetActive(false);

        pcTogBtn.Select();
    }
}
