using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OnClickEvent : UnityEvent { }

/// <summary>
/// 选钮 本质上是一个Toggle，扩展出一组可以互相关联的Button
/// </summary>
[RequireComponent(typeof(Toggle))]
[RequireComponent(typeof(CanvasGroup))]
public class TogButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //public Action OnClickEvent;
    private OnClickEvent onClick = new OnClickEvent();
    private GameObject inControlGo;//被控制的物体

    public Toggle toggle;
    public ToggleGroup toggleGroup;

    private bool isSelect = false;
    public bool IsSelect
    {
        get
        {
            return isSelect;
        }
        set
        {
            isSelect = value;
            if (isSelect)
            {
                ApplayColor(highLightColor);
            }
            else
            {
                ApplayColor(normalColor);
            }
            ApplyController();
        }
    }

    [Header("需要改变颜色的UI")]
    public Graphic[] graphics;
    public Color normalColor = new Color(155, 155, 155, 255);
    public Color highLightColor = Color.white;
    public float normalAlaph = 1;
    public float highLightAlaph = 1;

    private CanvasGroup canvasGroup;

    /// <summary>
    /// 使用预初始化代替Awake
    /// </summary>
    private void PreInit()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = normalAlaph;
        ApplayColor(normalColor);
    }

    /// <summary>
    /// 初始化选钮
    /// </summary>
    public void Init(ToggleGroup group, GameObject controlGo = null)
    {
        PreInit();
        BindGroup(group);
        if (controlGo)
        {
            inControlGo = controlGo;
        }
    }

    private void BindGroup(ToggleGroup group)
    {
        toggle.group = group;
        toggleGroup = group;
    }

    public void AddListener(UnityAction action)
    {
        onClick.AddListener(action);
    }

    /// <summary>
    /// 所控制物体的显隐
    /// </summary>
    private void ApplyController()
    {
        if (inControlGo)
        {
            inControlGo.SetActive(isSelect);
        }
    }

    private void ApplayColor(Color color)
    {
        if (graphics.Length == 0)
        {
            return;
        }

        for (int i = 0; i < graphics.Length; i++)
        {
            graphics[i].color = color;
        }
    }

    private void OnToggleValueChanged(bool isOn)
    {
        IsSelect = isOn;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (toggle.IsInteractable() && !isSelect)
        {
            canvasGroup.alpha = highLightAlaph;
            ApplayColor(highLightColor);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (toggle.IsInteractable() && !isSelect)
        {
            canvasGroup.alpha = normalAlaph;
            ApplayColor(normalColor);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (toggle.IsInteractable() && !isSelect)
        {
            onClick?.Invoke();
        }
    }
}

public static class TogButtonEx
{
    public static void Select(this TogButton togButton)
    {
        if (togButton)
        {
            togButton.toggle.isOn = true;
        }
    }
}