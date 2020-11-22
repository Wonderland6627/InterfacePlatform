using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ImageButton : MonoBehaviour, IPointerClickHandler
{
    public Color enableColor = Color.green;
    public Color disableColor = Color.red;

    public Image image;
    public Text text;

    public UnityEvent OnClick;

    private bool enable = true;
    public bool Enable
    {
        get
        {
            return enable;
        }
        set
        {
            enable = value;
            image.color = enable ? enableColor : disableColor;
        }
    }

    private void Awake()
    {
        if(!GetComponent<Image>())
        {
            gameObject.AddComponent<Image>();
        }
        image = GetComponent<Image>();

        if (GetComponent<Text>())
        {
            text = GetComponent<Text>();
        }
    }

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
