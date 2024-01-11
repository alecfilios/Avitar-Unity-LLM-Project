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

    public override void Init()
    {
        double fat = HealthDataUtils.CalculateSumForQuantityType(HKDataType.HKQuantityTypeIdentifierDietaryFatTotal);
        DietaryFatTotalText.text = "Dietary Fat Total:" + fat;

        double carbohydrates = HealthDataUtils.CalculateSumForQuantityType(HKDataType.HKQuantityTypeIdentifierDietaryCarbohydrates);
        DietaryCarbohydratesText.text = "Dietary Carbohydrates:" + carbohydrates;

        double protein = HealthDataUtils.CalculateSumForQuantityType(HKDataType.HKQuantityTypeIdentifierDietaryProtein);
        DietaryProteinText.text = "Dietary Protein:" + protein;

        double water = HealthDataUtils.CalculateSumForQuantityType(HKDataType.HKQuantityTypeIdentifierDietaryWater);
        DietaryWaterText.text = "Dietary Water:" + water;
    }
}
