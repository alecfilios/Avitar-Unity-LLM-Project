using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using BeliefEngine.HealthKit;
using UnityEngine;

public class FakeCategorySample : CategorySample
{
    public FakeCategorySample(string key) : base()
    {
        System.Random random = new System.Random();

        switch ((HKDataType)Enum.Parse(typeof(HKDataType), key))
        {
            // ------------------------------------------------------------------------------------------------
            // [Heart]
            // ------------------------------------------------------------------------------------------------
            case HKDataType.HKCategoryTypeIdentifierHighHeartRateEvent:
                value = 0;
                startDate = GenerateRandomDate(DateTimeOffset.UtcNow.AddDays(-1), DateTimeOffset.UtcNow);
                endDate = GenerateRandomDate(startDate, DateTimeOffset.UtcNow);
                break;
            case HKDataType.HKCategoryTypeIdentifierLowHeartRateEvent:
                value = 0;
                startDate = GenerateRandomDate(DateTimeOffset.UtcNow.AddDays(-1), DateTimeOffset.UtcNow);
                endDate = GenerateRandomDate(startDate, DateTimeOffset.UtcNow);
                break;
            case HKDataType.HKCategoryTypeIdentifierIrregularHeartRhythmEvent:
                value = 0;
                startDate = GenerateRandomDate(DateTimeOffset.UtcNow.AddDays(-1), DateTimeOffset.UtcNow);
                endDate = GenerateRandomDate(startDate, DateTimeOffset.UtcNow);
                break;

            // Add more cases for other category types if needed
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

