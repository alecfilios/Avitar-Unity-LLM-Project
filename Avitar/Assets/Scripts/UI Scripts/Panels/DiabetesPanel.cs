using System;
using System.Collections;
using System.Collections.Generic;
using BeliefEngine.HealthKit;
using TMPro;
using UnityEngine;

public class DiabetesPanel : OptionsPanelBase
{

    [SerializeField]
    TMP_Text BloodGlucoseText;
    [SerializeField]
    TMP_Text InsulinDeliveryNumberText;

    [SerializeField]
    ParticleSystem CircleParticleSystem;

    [SerializeField]
    ParticleSystem AuraParticleSystem;

    private void OnEnable()
    {

        CircleParticleSystem.gameObject.SetActive(true);
        CircleParticleSystem.Play();
    }
    private void OnDisable()
    {
        CircleParticleSystem.Stop();
        CircleParticleSystem.gameObject.SetActive(false);
    }
    public override void Init()
    {
        int insulinDeliveryCounter = 0;

        List<QuantitySample> samples = HealthDataUtils.GetQuantitySamplesByType(HKDataType.HKQuantityTypeIdentifierInsulinDelivery);

        foreach (var sample in samples)
        {
            //insulinDelivery += FormatQuantity(sample.quantity.doubleValue, sample.quantity.unit, 0) + " (" + HealthDataUtils.FormatDate(sample.startDate) + " - " + HealthDataUtils.FormatDate(sample.endDate) + ")\n";
            insulinDeliveryCounter += 1;
        }
        //InsulinDeliveryText.text = insulinDelivery;
        InsulinDeliveryNumberText.text = insulinDeliveryCounter.ToString();
        UserData.Instance.SetPlayerProfileFieldValue("insulin_deliveries", insulinDeliveryCounter.ToString());


        double bloodGlucose = HealthDataUtils.GetMostRecentQuantityValue(HKDataType.HKQuantityTypeIdentifierBloodGlucose);

        BloodGlucoseText.text = "Blood Glucose: " + FormatQuantity(bloodGlucose, "mmol/L", 0);
        UserData.Instance.SetPlayerProfileFieldValue("blood_glucose", FormatQuantity(bloodGlucose, "mmol/L", 0));

    }

    private string FormatQuantity(double value, string unit, int digits)
    {
        // Round the value to a number of decimal places and append the unit
        return $"{Math.Round(value, digits)} {unit}";
    }
}
