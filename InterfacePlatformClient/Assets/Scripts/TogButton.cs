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
public class TogButton : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //public Action OnClickEvent;
    private OnClickEvent onClick = new OnClickEvent();
    protected GameObject inControlGo;//被控制的物体

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
                ApplyStateChanged(clickedSprite, clcikedColor);
            }
            else
            {
                ApplyStateChanged(normalSprite, normalColor);
            }
            OnApplySelected();
        }
    }

    [Header("需要改变图片属性的UI")]
    public Image[] imageGraphics;
    public Sprite normalSprite;
    public Sprite clickedSprite;

    [Header("需要改变颜色的UI")]
    public Graphic[] colorGraphics;
    public Color normalColor = new Color(155, 155, 155, 255);
    public Color clcikedColor = Color.white;

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
        ApplyStateChanged(normalSprite, normalColor);
    }

    /// <summary>
    /// 初始化选钮
    /// </summary>
    public virtual void Init(object param = null)
    {
        PreInit();
    }  

    public virtual void Init(ToggleGroup group, object param = null)
    {
        Init();
        BindGroup(group);
    }

    /// <summary>
    /// Bind方法必须在base.Init()之后调用
    /// </summary>
    /// <param name="group"></param>
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
    /// 点击事件
    /// </summary>
    public virtual void OnApplySelected()
    {
        
    }

    private void ApplyStateChanged(Sprite sprite, Color color)
    {
        if (imageGraphics.Length > 0)
        {
            for (int i = 0; i < imageGraphics.Length; i++)
            {
                if (sprite != null)
                {
                    imageGraphics[i].sprite = sprite;
                }
            }
        }

        if (colorGraphics.Length > 0)
        {
            for (int i = 0; i < colorGraphics.Length; i++)
            {
                colorGraphics[i].color = color;
            }
        }
    }

    private void OnToggleValueChanged(bool isOn)
    {
        IsSelect = isOn;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (toggle && toggle.IsInteractable() && !isSelect)
        {
            canvasGroup.alpha = highLightAlaph;
            ApplyStateChanged(clickedSprite, clcikedColor);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (toggle && toggle.IsInteractable() && !isSelect)
        {
            canvasGroup.alpha = normalAlaph;
            ApplyStateChanged(normalSprite, normalColor);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (toggle && toggle.IsInteractable() && !isSelect)
        {
            onClick?.Invoke();
        }
    }
}

public static class TogButtonEx
{
    public static void Select(this OptionTogButton togButton)
    {
        if (togButton)
        {
            togButton.toggle.isOn = true;
        }
    }
}