﻿/********************************************************************
	purpose:	遵循Mono继承线的单件实现类（非线程安全）
*********************************************************************/
using System.Collections.Generic;
using UnityEngine;

public class GenericSingletonStat
{
    public delegate void DestroyDelegate();
    public static HashSet<DestroyDelegate> DestroyInstanceDelegate = new HashSet<DestroyDelegate>();
}

/// <summary>
/// 基类继承树中有MonoBehaviour类的单件实现，这种单件实现有利于减少对场景树的查询操作
/// </summary>
public class GenericSingleton<T> : MonoBehaviour where T : Component
{
    // 单件子类实例
    private static T _instance;

    public static T Instance
    {
        get
        {
            return GetInstance();
        }
    }

    /// <summary>
    /// Awake消息，确保单件实例的唯一性, 删除新创建的instance, 要确保子类不要使用Awake
    /// </summary>
    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = GetComponent<T>();
            DontDestroyOnLoad(gameObject);
        }

        GenericSingletonStat.DestroyInstanceDelegate.Add(DestroyInstance);
    }

    // 在单件中，每个物件的destroyed标志设计上应该分割在不同的存储个空间中，因此，忽略R#的这个提示
    // ReSharper disable once StaticFieldInGenericType
    // private static bool _destroyed;

    public static bool IsValidate()
    {
        return _instance != null;
    }

    /// <summary>
    /// 获得单件实例，查询场景中是否有该种类型，如果有存储静态变量，如果没有，构建一个带有这个component的gameobject
    /// 这种单件实例的GameObject直接挂接在bootroot节点下，在场景中的生命周期和游戏生命周期相同，创建这个单件实例的模块
    /// 必须通过DestroyInstance自行管理单件的生命周期
    /// </summary>
    /// <returns>返回单件实例</returns>
    public static T GetInstance()
    {
        if (_instance == null/* && !_destroyed*/)
        {
            _instance = (T)FindObjectOfType(typeof(T));
            if (_instance == null)
            {
                var go = new GameObject(typeof(T).Name);

                _instance = go.AddComponent<T>();

                if (Application.isPlaying) // 防止编辑器内使用出错
                {
                    DontDestroyOnLoad(go);
                }
            }
        }
        return _instance;
    }

    /// <summary>
    /// 删除单件实例,这种继承关系的单件生命周期应该由模块显示管理
    /// </summary>
    public static void DestroyInstance()
    {
        if (_instance != null)
        {
            Destroy(_instance.gameObject);
        }
        _instance = null;
    }

    /// <summary>
    /// OnDestroy消息，确保单件的静态实例会随着GameObject销毁
    /// </summary>
    protected virtual void OnDestroy()
    {
        if (_instance != null && _instance.gameObject == gameObject)
        {
            _instance = null;
        }
        //_destroyed = true;
    }
}