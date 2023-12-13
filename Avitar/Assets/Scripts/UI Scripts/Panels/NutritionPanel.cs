using System.Collections;
using System.Collections.Generic;
using BeliefEngine.HealthKit;
using TMPro;
using UnityEngine;

public class NutritionPanel : OptionsPanelBase
{
    [SerializeField]
    TMP_Text DietaryFatTotalText;
    [SerializeField]
    TMP_Text DietaryCarbohydratesText;
    [SerializeField]
    TMP_Text DietaryProteinText;
    [SerializeField]
    TMP_Text DietaryWaterText;

    public override void FillTexts()
    {
        DietaryFatTotalText.text = "Dietary Fat Total:" + UserData.Instance.QuantityTypeValues[HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierDietaryFatTotal)];
        DietaryCarbohydratesText.text = "Dietary Carbohydrates:" + UserData.Instance.QuantityTypeValues[HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierDietaryCarbohydrates)];
        DietaryProteinText.text = "Dietary Protein:" + UserData.Instance.QuantityTypeValues[HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierDietaryProtein)];
        DietaryWaterText.text = "Dietary Water:" + UserData.Instance.QuantityTypeValues[HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierDietaryWater)];
    }
}
