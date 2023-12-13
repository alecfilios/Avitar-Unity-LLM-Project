using System.Collections;
using System.Collections.Generic;
using BeliefEngine.HealthKit;
using TMPro;
using UnityEngine;

public class DiabetesPanel : OptionsPanelBase
{
    [SerializeField]
    TMP_Text InsulinDeliveryText;
    [SerializeField]
    TMP_Text BloodGlucoseText;

    public override void FillTexts()
    {

        QuantitySample insulinDelivery = UserData.Instance.QuantityTypeValues[HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierInsulinDelivery)][0];
        InsulinDeliveryText.text = 
        "Insulin Delivery: " + 
        insulinDelivery.quantity.doubleValue + 
        " between " + 
        insulinDelivery.startDate + 
        " and " + 
        insulinDelivery.endDate;
        BloodGlucoseText.text = "Blood Glucose:" + UserData.Instance.QuantityTypeValues[HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierBloodGlucose)];
    }
}
