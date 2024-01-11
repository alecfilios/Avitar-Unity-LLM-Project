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

    [SerializeField]
    Island HomeIsland;

    public void OnClickContinue()
    {
        GameManager.CallLoadDataFromHealthKitAsync();
        InfoPanel.SetActive(false);
        Camera.main.GetComponent<CameraController>().MoveToTargetByLocation(HomeIsland);

    }

    public void InitPanels()
    {
        foreach (var panel in OptionPanels)
        {
            panel.Init();
        }
    }
}
