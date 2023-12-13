using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

[System.Serializable]
public class RadialMenuEntryData
{
    public string title;
    public Texture icon;
    public GameObject panelPrefab;
}

public class RadialMenu : MonoBehaviour
{
    [SerializeField]
    GameObject _entryPrefab;

    [SerializeField]
    List<RadialMenuEntryData> _menuEntries;

    List<RadialMenuEntry> _entries;

    [SerializeField]
    RawImage _targetIcon;

    [SerializeField]
    float _radius = 300f;

    private void Start()
    {
        _entries = new List<RadialMenuEntry>();
    }

    void AddEntry(RadialMenuEntryData entryData, RadialMenuEntry.RadialMenuEntryDelegate callback)
    {
        GameObject entry = Instantiate(_entryPrefab, transform);
        RadialMenuEntry radialMenuEntry = entry.GetComponent<RadialMenuEntry>();

        radialMenuEntry.SetData(entryData);
        radialMenuEntry.setCallback(callback);

        _entries.Add(radialMenuEntry);
    }

    void Open()
    {
        for (int i = 0; i < _menuEntries.Count; i++)
        {
            AddEntry(_menuEntries[i], SetUI);
        }
        Rearrange();
    }

    void Close()
    {
        for (int i = 0; i < _menuEntries.Count; i++)
        {
            RectTransform rect = _entries[i].GetComponent<RectTransform>();
            GameObject entry = _entries[i].gameObject;
            rect.localScale = Vector3.one;
            rect.DOScale(Vector3.zero, .3f).SetEase(Ease.OutQuad).SetDelay(.05f * i);
            rect.DOAnchorPos(Vector3.zero, .3f).SetEase(Ease.OutQuad).SetDelay(.05f * i).onComplete =
                delegate ()
                {
                    Destroy(entry);
                };

 
        }
        _entries.Clear();

        // Disable all panels when closing the menu
        DisableAllPanels();
    }

    // Helper method to disable all panels
    void DisableAllPanels()
    {
        foreach (var entry in _entries)
        {
            if (entry.GetData().panelPrefab != null)
            {
                entry.GetData().panelPrefab.SetActive(false);
            }
        }
    }



    public void Toggle()
    {
        if(_entries.Count == 0)
        {
            Open();
        }
        else
        {
            Close();
        }
    }


    void Rearrange()
    {
        float radiansOfSeperation = (Mathf.PI * 2) / _entries.Count;
        for(int i=0; i<_entries.Count; i++)
        {
            float x = Mathf.Sin(radiansOfSeperation * i) * _radius;
            float y = Mathf.Cos(radiansOfSeperation * i) * _radius;

            //_entries[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(x, y, 0);
            RectTransform rect = _entries[i].GetComponent<RectTransform>();

            rect.localScale = Vector3.zero;
            rect.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad).SetDelay(.05f * i);
            _entries[i].GetComponent<RectTransform>().DOAnchorPos(new Vector3(x, y, 0), .3f).SetEase(Ease.OutQuad).SetDelay(.05f * i);

        }
    }

    private void SetUI(RadialMenuEntry entry)
    {
        _targetIcon.texture = entry.GetIcon();

        // Disable all panels when a new entry is selected
        DisableAllPanels();

        // Enable the panel if it exists in the data
        if (entry.GetData().panelPrefab != null)
        {
            entry.GetData().panelPrefab.SetActive(true);
        }

        Close();
    }
}
