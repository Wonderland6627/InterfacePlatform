using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingDrag : MonoBehaviour, IDragHandler
{
    public RectTransform dragFloating;

    public void OnDrag(PointerEventData eventData)
    {
        dragFloating.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }
}
