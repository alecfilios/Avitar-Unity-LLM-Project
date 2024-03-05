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
    }

    private void OnEnable()
    {

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
        playerController2D.SendText("I want you to make an analysis on my state of mind");
    }

    public void OnClickTriggerChat(bool trigger)
    {
        InWorldUI.SetActive(trigger);
        ShowInWorldUIButton.SetActive(!trigger);
    }
}
