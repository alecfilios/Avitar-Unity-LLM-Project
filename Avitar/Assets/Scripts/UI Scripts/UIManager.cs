using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using BeliefEngine.HealthKit;

public class UIManager : MonoBehaviour
{
    [Header("General")]

    [SerializeField]
    GameManager GameManager;

    [SerializeField]
    OptionsPanelBase[] OptionPanels;

    [SerializeField]
    GameObject RadialMenu;

    [Header("Intro")]
    [SerializeField]
    GameObject IntroPanel;

    [SerializeField]
    Island HomeIsland;

    [Header("Info")]
    [SerializeField]
    GameObject InfoPanel;

    [SerializeField]
    TMP_Text InfoTitle;

    [SerializeField]
    TMP_Text InfoDescription;

    private void Awake()
    {
        IntroPanel.SetActive(true);
        InfoPanel.SetActive(false);
        RadialMenu.SetActive(false);
    }


    public void OnClickContinue()
    {
        GameManager.CallLoadDataFromHealthKitAsync();
        IntroPanel.SetActive(false);
        Camera.main.GetComponent<CameraController>().MoveToTargetByLocation(HomeIsland);
        RadialMenu.SetActive(true);

    }

    public void OnClickOKInfo()
    {
        InfoPanel.SetActive(false);
        InfoTitle.text = "";
        InfoDescription.text = "";
    }

    public void OnClickInfo(string title, string description)
    {
        InfoTitle.text = title;
        InfoDescription.text = description;
        InfoPanel.SetActive(true);
    }

    public void InitPanels()
    {
        foreach (var panel in OptionPanels)
        {
            panel.Init();
        }
    }
}
