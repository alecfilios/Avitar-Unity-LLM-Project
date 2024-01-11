using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using BeliefEngine.HealthKit;
using UnityEngine;

public class FakeQuantitySample : QuantitySample
{
    public FakeQuantitySample(DateTimeOffset start, DateTimeOffset end, string key) : base()
    {
        System.Random random = new System.Random();

        switch ((HKDataType)Enum.Parse(typeof(HKDataType), key))
        {
            // ------------------------------------------------------------------------------------------------
            // [Body Measurements]
            // ------------------------------------------------------------------------------------------------
            case HKDataType.HKQuantityTypeIdentifierHeight:
                quantity = new Quantity
                {
                    unit = "meters",
                    doubleValue = UnityEngine.Random.Range(1.6f, 1.9f) // Random height between 1.6m and 1.9m
                };
                startDate = GenerateRandomDate(start, end);
                endDate = GenerateRandomDate(startDate, end);
                break;
            case HKDataType.HKQuantityTypeIdentifierBodyMass:
                quantity = new Quantity
                {
                    unit = "kg",
                    doubleValue = UnityEngine.Random.Range(60f, 80f) // Random body mass between 60kg and 80kg
                };
                startDate = GenerateRandomDate(start, end);
                endDate = GenerateRandomDate(startDate, end);
                break;

            // ------------------------------------------------------------------------------------------------
            // [Activity]
            // ------------------------------------------------------------------------------------------------
            case HKDataType.HKQuantityTypeIdentifierStepCount:
                int stepsPerSample = UnityEngine.Random.Range(500, 1500); // Random number of steps per instance
                quantity = new Quantity
                {
                    unit = "steps",
                    doubleValue = stepsPerSample
                };
                startDate = GenerateRandomDate(start, end);
                endDate = GenerateRandomDate(startDate, end);
                break;
            case HKDataType.HKQuantityTypeIdentifierDistanceWalkingRunning:
                float distancePerSample = UnityEngine.Random.Range(0.1f, 0.5f); // Random distance per instance in km
                quantity = new Quantity
                {
                    unit = "km",
                    doubleValue = distancePerSample
                };
                startDate = GenerateRandomDate(start, end);
                endDate = GenerateRandomDate(startDate, end);
                break;

            // ------------------------------------------------------------------------------------------------
            // [Heart]
            // ------------------------------------------------------------------------------------------------
            case HKDataType.HKQuantityTypeIdentifierHeartRate:
                quantity = new Quantity
                {
                    unit = "bpm",
                    doubleValue = UnityEngine.Random.Range(50, 100) // Random heart rate between 70bpm and 90bpm
                };
                startDate = GenerateRandomDate(start, end);
                endDate = GenerateRandomDate(startDate, end);
                break;

            // ------------------------------------------------------------------------------------------------
            // [Diabetes]
            // ------------------------------------------------------------------------------------------------
            case HKDataType.HKQuantityTypeIdentifierBloodGlucose:
                quantity = new Quantity
                {
                    unit = "mmol/L",
                    doubleValue = UnityEngine.Random.Range(4.0f, 6.5f) // Random blood glucose between 4.0mmol/L and 6.5mmol/L
                };
                startDate = GenerateRandomDate(start, end);
                endDate = GenerateRandomDate(startDate, end);
                break;
            case HKDataType.HKQuantityTypeIdentifierInsulinDelivery:
                quantity = new Quantity
                {
                    unit = "IU",
                    doubleValue = UnityEngine.Random.Range(8, 12) // Random insulin delivery between 8IU and 12IU
                };
                startDate = GenerateRandomDate(start, end);
                endDate = GenerateRandomDate(startDate, end);
                break;

            // ------------------------------------------------------------------------------------------------
            // [Nutrition]
            // ------------------------------------------------------------------------------------------------
            case HKDataType.HKQuantityTypeIdentifierDietaryFatTotal:
                quantity = new Quantity
                {
                    unit = "grams",
                    doubleValue = UnityEngine.Random.Range(5f, 15f) // Random dietary fat per instance in grams
                };
                startDate = GenerateRandomDate(start, end);
                endDate = GenerateRandomDate(startDate, end);
                break;
            case HKDataType.HKQuantityTypeIdentifierDietaryCarbohydrates:
                quantity = new Quantity
                {
                    unit = "grams",
                    doubleValue = UnityEngine.Random.Range(20f, 40f) // Random dietary carbohydrates per instance in grams
                };
                startDate = GenerateRandomDate(start, end);
                endDate = GenerateRandomDate(startDate, end);
                break;
            case HKDataType.HKQuantityTypeIdentifierDietaryProtein:
                quantity = new Quantity
                {
                    unit = "grams",
                    doubleValue = UnityEngine.Random.Range(15f, 30f) // Random dietary protein per instance in grams
                };
                startDate = GenerateRandomDate(start, end);
                endDate = GenerateRandomDate(startDate, end);
                break;
            case HKDataType.HKQuantityTypeIdentifierDietaryWater:
                float waterPerSample = UnityEngine.Random.Range(0.5f, 2.0f); // Random dietary water per instance in liters
                quantity = new Quantity
                {
                    unit = "liters",
                    doubleValue = waterPerSample
                };
                startDate = GenerateRandomDate(start, end);
                endDate = GenerateRandomDate(startDate, end);
                break;

            // Add more cases for other quantity types if needed
            default:
                break;
        }
    }

    private static DateTimeOffset GenerateRandomDate(DateTimeOffset start, DateTimeOffset end)
    {
        System.Random random = new System.Random();
        long range = end.Ticks - start.Ticks;
        long randomTicks = start.Ticks + (long)(random.NextDouble() * range);
        return new DateTimeOffset(randomTicks, TimeSpan.Zero);
    }
}






