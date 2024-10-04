using System.Collections;
using System.Collections.Generic;
using Inworld.Sample;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class HomePanel : OptionsPanelBase
{
    [Header("AI")]

    [SerializeField]
    Avatar avatar;

    [SerializeField]
    ChatPanel chatPanel;

    [SerializeField]
    GameObject InWorldUI;

    [SerializeField]
    Button StateOfMindButton;

    [SerializeField]
    GameObject ShowInWorldUIButton;

    [SerializeField]
    PlayerController2D playerController2D;

    [SerializeField]
    ScrollRect scrollRect;
    [SerializeField]
    GameObject okayButton;
    [SerializeField]
    Transform contents;

    [SerializeField]
    GameObject InputPanel;

    [SerializeField]
    Button MicrophoneButton;

    [SerializeField]
    Button SendChatButton;

    public override void Init()
    {
        TriggerInteractionButtons(false);
        string healthData = UserData.Instance.GetHealthDictionaryFormatted();
        chatPanel.SetHealthData(healthData);
        Debug.Log(healthData);
        playerController2D.SetHealthData(healthData);
    }

    public void OnValueChanged()
    {
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }

    public void OnClickOkay()
    {
        ResetContents();
        okayButton.SetActive(false);
        InputPanel.SetActive(true);
        TriggerInteractionButtons(true);
    }

    public void ResetContents()
    {
        for (int i = 0; i < contents.childCount; i++)
        {
            Destroy(contents.GetChild(i).gameObject);
        }
    }

    public void TriggerInteractionButtons(bool trigger)
    {
        SendChatButton.interactable = trigger;
        MicrophoneButton.interactable = trigger;
        StateOfMindButton.interactable = trigger;
    }

    public void OnClickStateOfMind()
    {
        StateOfMindButton.interactable = false;
        string hrvData = avatar.OnGoalComplete("initiate_hrv_analysis");
        
    }

    public void OnClickTriggerChat(bool trigger)
    {
        InWorldUI.SetActive(trigger);
        ShowInWorldUIButton.SetActive(!trigger);
    }
}
