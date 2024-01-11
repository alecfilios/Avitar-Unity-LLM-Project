using System;
using System.Collections.Generic;
using BeliefEngine.HealthKit;

public static class HealthDataUtils
{
    // Example of calculating the sum for a specific quantity type
    public static double CalculateSumForQuantityType(HKDataType quantityType)
    {
        List<QuantitySample> quantitySamples = GetQuantitySamplesByType(quantityType);
        return CalculateSum(quantitySamples);
    }

    // Example of getting quantity samples for a specific type
    public static List<QuantitySample> GetQuantitySamplesByType(HKDataType quantityType)
    {
        return GetSamplesByType<QuantitySample>(quantityType);
    }

    // Example of getting category samples for a specific category type
    public static List<CategorySample> GetCategorySamplesByType(HKDataType categoryType)
    {
        return GetSamplesByType<CategorySample>(categoryType);
    }

    // Example of getting a characteristic for a specific characteristic type
    public static Characteristic GetCharacteristicByType(HKDataType characteristicType)
    {
        string typeName = HealthKitDataTypes.GetIdentifier(characteristicType);

        if (UserData.Instance.CharacteristicTypeValues.TryGetValue(typeName, out Characteristic characteristic))
        {
            return characteristic;
        }

        // If the type is not found, return null or handle it as needed
        return null;
    }

    // General method for getting samples or a characteristic by type
    private static List<T> GetSamplesByType<T>(HKDataType dataType) where T : class
    {
        string typeName = HealthKitDataTypes.GetIdentifier(dataType);

        if (typeof(T) == typeof(QuantitySample))
        {
            if (UserData.Instance.QuantityTypeValues.TryGetValue(typeName, out var samples))
            {
                return samples as List<T>;
            }
        }
        else if (typeof(T) == typeof(CategorySample))
        {
            if (UserData.Instance.CategoryTypeValues.TryGetValue(typeName, out var samples))
            {
                return samples as List<T>;
            }
        }
        else if (typeof(T) == typeof(Characteristic))
        {
            if (UserData.Instance.CharacteristicTypeValues.TryGetValue(typeName, out var characteristic))
            {
                return new List<T> { characteristic as T };
            }
        }

        // If the type is not found or doesn't match the expected types, return an empty list or handle it as needed
        return new List<T>();
    }

    // Example of calculating the sum
    private static double CalculateSum(List<QuantitySample> quantitySamples)
    {
        double sum = 0;
        foreach (QuantitySample sample in quantitySamples)
        {
            sum += sample.quantity.doubleValue;
        }
        return sum;
    }

    public static double CalculateAverageForQuantityType(HKDataType quantityType)
    {
        return CalculateSumForQuantityType(quantityType) / GetQuantitySamplesByType(quantityType).Count; ;
    }

    public static double GetMostRecentQuantityValue(HKDataType quantityType)
    {
        return GetQuantitySamplesByType(quantityType)[GetQuantitySamplesByType(quantityType).Count - 1].quantity.doubleValue;
    }

    public static string FormatDate(DateTimeOffset date)
    {
        TimeSpan timeAgo = DateTime.UtcNow - date.UtcDateTime;

        if (timeAgo.TotalDays < 1)
        {
            return date.ToString("HH:mm");
        }
        else
        {
            return date.ToString("yyyy-MM-dd HH:mm");
        }
    }

}
