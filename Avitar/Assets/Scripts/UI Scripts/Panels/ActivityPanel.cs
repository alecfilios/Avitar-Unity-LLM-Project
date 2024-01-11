using System;
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



    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    public override void Init()
    {
        double steps = HealthDataUtils.CalculateSumForQuantityType(HKDataType.HKQuantityTypeIdentifierStepCount);
        StepCountText.text = FormatQuantity(steps, "");

        double distanceWalkingRunning = HealthDataUtils.CalculateSumForQuantityType(HKDataType.HKQuantityTypeIdentifierDistanceWalkingRunning);
        DistanceWalkingRunningText.text = FormatQuantity(distanceWalkingRunning, "km");
    }

    private string FormatQuantity(double value, string unit)
    {
        // Round the value to 2 decimal places and append the unit
        return $"{Math.Round(value, 1)} {unit}";
    }
}
