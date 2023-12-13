using System.Collections;
using System.Collections.Generic;
using BeliefEngine.HealthKit;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ActivityPanel : OptionsPanelBase
{
    [SerializeField]
    TMP_Text StepCountText;
    [SerializeField]
    TMP_Text DistanceWalkingRunningText;

    public override void FillTexts()
    {
        double steps = HealthDataUtils.CalculateSumForQuantityType(HKDataType.HKQuantityTypeIdentifierStepCount);
        StepCountText.text = "Steps Count: " + steps;
        double distanceWalkingRunning = HealthDataUtils.CalculateSumForQuantityType(HKDataType.HKQuantityTypeIdentifierDistanceWalkingRunning);
        DistanceWalkingRunningText.text = "Distance Walking Running: " + distanceWalkingRunning;
    }
}