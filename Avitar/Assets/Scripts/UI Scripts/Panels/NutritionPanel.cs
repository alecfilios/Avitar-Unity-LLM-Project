using System;
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
        DietaryFatTotalText.text = FormatQuantity(fat, "g", 1);

        double carbohydrates = HealthDataUtils.CalculateSumForQuantityType(HKDataType.HKQuantityTypeIdentifierDietaryCarbohydrates);
        DietaryCarbohydratesText.text = FormatQuantity(carbohydrates, "g", 1);

        double protein = HealthDataUtils.CalculateSumForQuantityType(HKDataType.HKQuantityTypeIdentifierDietaryProtein);
        DietaryProteinText.text = FormatQuantity(protein, "g", 1);

        double water = HealthDataUtils.CalculateSumForQuantityType(HKDataType.HKQuantityTypeIdentifierDietaryWater);
        DietaryWaterText.text = FormatQuantity(water, "ml", 1);
    }

    private string FormatQuantity(double value, string unit, int digits)
    {
        // Round the value to 1 decimal place and append the unit
        return $"{Math.Round(value, digits)} {unit}";
    }
}
