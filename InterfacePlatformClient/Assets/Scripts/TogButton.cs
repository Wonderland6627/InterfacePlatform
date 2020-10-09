using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 本质上是一个Toggle，扩展出一组可以互相关联的Button
/// </summary>
public class TogButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Toggle toggle;

    public Color normalColor;
    public Color highLightColor;
    public float normalAlaph;
    public float highLightAlaph;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        canvasGroup = GetComponent<CanvasGroup>();
        if(canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(toggle.IsInteractable())
        {
            canvasGroup.alpha = highLightAlaph;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (toggle.IsInteractable())
        {
            canvasGroup.alpha = normalAlaph;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(toggle.IsInteractable())
        {
            Debug.Log(gameObject.name);
        }
    }
}
