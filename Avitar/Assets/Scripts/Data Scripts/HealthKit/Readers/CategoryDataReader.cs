using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

namespace BeliefEngine.HealthKit
{
    public class CategoryDataReader : DataReader
    {
        public override IEnumerator ReadDataAsync()
        {
            // We'll read everything from the past 24 hours
            DateTimeOffset now = DateTimeOffset.UtcNow;
            DateTimeOffset start = now.AddDays(-1);
            var categoryKeyList = new List<string>(UserData.Instance.CategoryTypeValues.Keys);

            foreach (var key in categoryKeyList)
            {
                HKDataType dataType = (HKDataType)Enum.Parse(typeof(HKDataType), key);
                yield return StartCoroutine(ReadValue(dataType, start, now));
            }
        }

        private IEnumerator ReadValue(HKDataType dataType, DateTimeOffset start, DateTimeOffset end)
        {
            string typeName = HealthKitDataTypes.GetIdentifier(dataType);
            Debug.LogFormat("[{0}]: Reading category data for [{1}].", nameof(CategoryDataReader), typeName);
            bool operationCompleted = false;
            this._healthStore.ReadCategorySamples(dataType, start, end, delegate (List<CategorySample> samples, Error error)
            {
                if (error != null)
                {
                    HandleReadingError(new Exception($"Error reading {typeName} quantity data: {error.localizedDescription}"));
                    operationCompleted = true;
                    return;
                }
                if (samples.Count > 0)
                {
                    UserData.Instance.CategoryTypeValues[typeName] = samples;
                }

                // Process category data
                operationCompleted = true;
            });




            // Wait for the read operation to complete
            while (!operationCompleted)
            {
                yield return null;
            }
        }
    }

}
