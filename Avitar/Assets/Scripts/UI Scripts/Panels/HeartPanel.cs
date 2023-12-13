using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BeliefEngine.HealthKit;

public class HeartPanel : OptionsPanelBase
{
    [SerializeField]
    TMP_Text HeartRateText;
    [SerializeField]
    TMP_Text HighHeartRateEventText;
    [SerializeField]
    TMP_Text LowHeartRateEventText;
    [SerializeField]
    TMP_Text IrregularHeartRhythmEventText;

    public override void FillTexts()
    {
        HeartRateText.text = "Heart Rate:" + UserData.Instance.QuantityTypeValues[HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierHeartRate)];
        HighHeartRateEventText.text = "High Heart Rate Event:" + UserData.Instance.CategoryTypeValues[HealthKitDataTypes.GetIdentifier(HKDataType.HKCategoryTypeIdentifierHighHeartRateEvent)];
        LowHeartRateEventText.text = "Low Heart Rate Event:" + UserData.Instance.CategoryTypeValues[HealthKitDataTypes.GetIdentifier(HKDataType.HKCategoryTypeIdentifierLowHeartRateEvent)];
        IrregularHeartRhythmEventText.text = "Irregular Heart Rhythm Event:" + UserData.Instance.CategoryTypeValues[HealthKitDataTypes.GetIdentifier(HKDataType.HKCategoryTypeIdentifierIrregularHeartRhythmEvent)];
    }
}
