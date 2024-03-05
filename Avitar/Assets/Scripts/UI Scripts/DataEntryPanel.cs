using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataEntryPanel : MonoBehaviour
{
    UIManager UIManager;
    [SerializeField]
    string title;
    [SerializeField]
    string description;

    private void Awake()
    {
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void OnClickInfo()
    {
        UIManager.OnClickInfo(title, description);
    }
}
