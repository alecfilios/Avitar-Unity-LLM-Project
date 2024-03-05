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
    public Island island;
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

    [SerializeField]
    Button MenuButton;

    private void Awake()
    {
        _entries = new List<RadialMenuEntry>();
    }


    private void Update()
    {
        if (Camera.main.GetComponent<CameraController>().isMoving)
        {
            MenuButton.interactable = false;
        }
        else
        {
            MenuButton.interactable = true;
        }
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
            RadialMenuEntry entry = _entries[i];
            RectTransform rect = entry?.GetComponent<RectTransform>();
            GameObject entryObject = entry?.gameObject;

            if (rect != null)
            {
                rect.localScale = Vector3.one;
                rect.DOScale(Vector3.zero, .3f).SetEase(Ease.OutQuad).SetDelay(.05f * i);
                rect.DOAnchorPos(Vector3.zero, .3f).SetEase(Ease.OutQuad).SetDelay(.05f * i).onComplete =
                    delegate ()
                    {
                        if (entryObject != null)
                        {
                            Destroy(entryObject); // Destroy the entire GameObject
                        }
                    };
            }
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
        if (_entries.Count == 0)
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

        for (int i = 0; i < _entries.Count; i++)
        {
            float x = Mathf.Sin(radiansOfSeperation * i) * _radius;
            float y = Mathf.Cos(radiansOfSeperation * i) * _radius;

            RectTransform rect = _entries[i]?.GetComponent<RectTransform>(); // Check if _entries[i] is not null

            if (rect != null)
            {
                rect.localScale = Vector3.zero;
                rect.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad).SetDelay(.05f * i);
                _entries[i]?.GetComponent<RectTransform>().DOAnchorPos(new Vector3(x, y, 0), .3f).SetEase(Ease.OutQuad).SetDelay(.05f * i);
            }
        }
    }

    private void SetUI(RadialMenuEntry entry)
    {
        if (entry == null)
        {
            Debug.LogError("Entry is null");
            return;
        }

        var icon = entry.GetIcon();
        if (icon == null)
        {
            Debug.LogError("Icon is null");
            return;
        }

        _targetIcon.texture = icon;

        // Disable all panels when a new entry is selected
        DisableAllPanels();

        var data = entry.GetData();
        if (data == null)
        {
            Debug.LogError("Data is null");
            return;
        }

        if (data.island == null)
        {
            Debug.LogError("Island is null");
            return;
        }

        Camera.main.GetComponent<CameraController>().MoveToTargetByLocation(data.island);

        // Enable the panel if it exists in the data
        if (data.panelPrefab != null)
        {
            data.panelPrefab.SetActive(true);
        }
        else
        {
            Debug.LogError("Panel prefab is null");
        }

        Close();
    }


}
