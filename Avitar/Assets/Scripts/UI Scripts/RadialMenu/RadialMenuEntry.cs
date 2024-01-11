using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class RadialMenuEntry : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void RadialMenuEntryDelegate(RadialMenuEntry entry);

    [SerializeField]
    TextMeshProUGUI _label;

    [SerializeField]
    RawImage _icon;

    RectTransform _rect;

    RadialMenuEntryDelegate _callback;

    RadialMenuEntryData _data;

    int targetIndex;


    private void Start()
    {
        _rect = _icon.GetComponent<RectTransform>();
    }

    public void SetData(RadialMenuEntryData data)
    {
        _data = data;
        _label.text = data.title;
        _icon.texture = data.icon;

    }

    public void SetLabel(string text)
    {
        _label.text = text;
    }

    public void SetIcon(Texture icon)
    {
        _icon.texture = icon;
    }

    public Texture GetIcon()
    {
        return (_icon.texture);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _callback?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _rect.DOComplete();
        _rect.DOScale(Vector3.one * 1.5f, 0.2f).SetEase(Ease.OutQuad);       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rect.DOComplete();
        _rect.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutQuad);
    }

    public void setCallback(RadialMenuEntryDelegate callback)
    {
        _callback = callback;
    }

    public RadialMenuEntryData GetData()
    {
        return _data;
    }
}
