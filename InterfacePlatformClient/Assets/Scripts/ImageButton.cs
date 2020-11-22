using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ImageButton : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnClick;

    public void AddListener(UnityAction onclick)
    {
        OnClick.AddListener(onclick);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClick != null)
        {
            OnClick.Invoke();
        }
    }
}
