using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using BeliefEngine.HealthKit;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameManager GameManager;

    [SerializeField]
    OptionsPanelBase[] OptionPanels;

    [SerializeField]
    GameObject InfoPanel;

    public void OnClickContinue()
    {
        GameManager.CallLoadDataFromHealthKitAsync();
        InfoPanel.SetActive(false);
    }

    public void InitializeTexts()
    {
        foreach(var panel in OptionPanels)
        {
            panel.FillTexts();
        }
    }
}
