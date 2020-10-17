using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemBoot : MonoBehaviour
{
    private void Awake()
    {
        InterfacePlatformTools.CreateJsonFiles();
    }
}
