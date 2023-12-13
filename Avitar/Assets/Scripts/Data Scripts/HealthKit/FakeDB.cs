using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using BeliefEngine.HealthKit;
using UnityEngine;

public static class FakeDB
{
    public static void GenerateFakeData()
    {
        GenerateQuantityData();
        GenerateCharacteristicData();
        GenerateCategoryData();
    }

    private static void GenerateQuantityData()
    {
        // We'll read everything from the past 24 hours
        DateTimeOffset end = DateTimeOffset.UtcNow;
        DateTimeOffset start = end.AddDays(-1);
        List<string> quantityKeys = new List<string>(UserData.Instance.QuantityTypeValues.Keys);
        foreach (var key in quantityKeys)
        {
            List<FakeQuantitySample> samples = GenerateFakeQuantitySamples(start, end);

            // Convert the list of FakeQuantitySample to a list of QuantitySample
            List<QuantitySample> baseSamples = samples.Cast<QuantitySample>().ToList();

            UserData.Instance.QuantityTypeValues[key] = baseSamples;
        }
    }

    private static List<FakeQuantitySample> GenerateFakeQuantitySamples(DateTimeOffset start, DateTimeOffset end)
    {
        List<FakeQuantitySample> samples = new List<FakeQuantitySample>();
        for (int i = 0; i < 10; i++)
        {
            FakeQuantitySample sample = new FakeQuantitySample();

            samples.Add(sample);
        }
        //int fakeValue = UnityEngine.Random.Range(1000, 5000);
        return samples;
    }

    private static void GenerateCategoryData()
    {
        List<string> categoryKeys = new List<string>(UserData.Instance.CategoryTypeValues.Keys);
        foreach (var key in categoryKeys)
        {
            List<FakeCategorySample> samples = GenerateFakeCategorySamples();

            // Convert the list of FakeCategorySample to a list of CategorySample
            List<CategorySample> baseSamples = samples.Cast<CategorySample>().ToList();

            UserData.Instance.CategoryTypeValues[key] = baseSamples;
        }
    }

    private static List<FakeCategorySample> GenerateFakeCategorySamples()
    {
        List<FakeCategorySample> samples = new List<FakeCategorySample>();
        for (int i = 0; i < 5; i++)
        {
            FakeCategorySample categorySample = new FakeCategorySample();
            samples.Add(categorySample);
        }
        return samples;
    }

    private static void GenerateCharacteristicData()
    {
        List<string> characteristicKeys = new List<string>(UserData.Instance.CharacteristicTypeValues.Keys);
        foreach (var key in characteristicKeys)
        {
            Characteristic characteristic = GenerateFakeCharacteristic(key);
            Debug.Log(characteristic);
            UserData.Instance.CharacteristicTypeValues[key] = characteristic;
        }
    }

    private static Characteristic GenerateFakeCharacteristic(string key)
    {
        System.Random random = new System.Random();

        switch ((HKDataType)Enum.Parse(typeof(HKDataType), key))
        {
            case HKDataType.HKCharacteristicTypeIdentifierBiologicalSex:
                return new BiologicalSexCharacteristic()
                {
                    value = GenerateRandomEnumValue<BiologicalSex>(random)
                };
            case HKDataType.HKCharacteristicTypeIdentifierBloodType:
                return new BloodTypeCharacteristic()
                {
                    value = GenerateRandomEnumValue<BloodType>(random)
                };
            case HKDataType.HKCharacteristicTypeIdentifierDateOfBirth:
                return new DateOfBirthCharacteristic()
                {
                    value = DateTimeOffset.UtcNow.AddYears(-random.Next(18, 80)) // Generate a random birthdate for testing
                };
            case HKDataType.HKCharacteristicTypeIdentifierFitzpatrickSkinType:
                return new FitzpatrickSkinTypeCharacteristic()
                {
                    value = GenerateRandomEnumValue<FitzpatrickSkinType>(random)
                };
            case HKDataType.HKCharacteristicTypeIdentifierWheelchairUse:
                return new WheelchairUseCharacteristic()
                {
                    value = GenerateRandomEnumValue<WheelchairUse>(random)
                };
            // Add more cases for other characteristics if needed
            default:
                return null;
        }
    }

    private static T GenerateRandomEnumValue<T>(System.Random random) where T : Enum
    {
        Type type = typeof(T);
        Array values = Enum.GetValues(type);
        int index = random.Next(values.Length);
        return (T)values.GetValue(index);
    }

    class FakeQuantitySample : QuantitySample
    {

        public FakeQuantitySample() : base()
        {
            System.Random random = new System.Random();
            quantityType = GenerateRandomEnumValue<QuantityType>(random);
            quantity = new Quantity
            {
                unit = "units",

                doubleValue = UnityEngine.Random.Range(1000, 5000)
            };
            mostRecentQuantity = UnityEngine.Random.Range(1000, 5000);
            minimumQuantity = UnityEngine.Random.Range(1000, 2500);
            maximumQuantity = UnityEngine.Random.Range(2500, 5000);
            averageQuantity = UnityEngine.Random.Range(2000, 3000);
            startDate = DateTimeOffset.UtcNow.AddDays(-1);
            endDate = DateTimeOffset.UtcNow;
        }
    }

    class FakeCategorySample : CategorySample
    {
        public FakeCategorySample() : base()
        {
            System.Random random = new System.Random();
            value = UnityEngine.Random.Range(0, 3);
            startDate = DateTimeOffset.UtcNow.AddDays(-1);
            endDate = DateTimeOffset.UtcNow;
        }
    }


}
