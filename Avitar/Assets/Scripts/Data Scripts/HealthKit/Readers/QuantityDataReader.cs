using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

namespace BeliefEngine.HealthKit
{
    public class QuantityDataReader : DataReader
    {
        public override IEnumerator ReadDataAsync()
        {
            // We'll read everything from the past 24 hours
            DateTimeOffset now = DateTimeOffset.UtcNow;
            DateTimeOffset start = now.AddDays(-1);

            var quantityKeyList = new List<string>(UserData.Instance.QuantityTypeValues.Keys);

            foreach (var key in quantityKeyList)
            {
                HKDataType dataType = (HKDataType)Enum.Parse(typeof(HKDataType), key);
                yield return StartCoroutine(ReadValue(dataType, start, now));
            }
        }

        private IEnumerator ReadValue(HKDataType dataType, DateTimeOffset start, DateTimeOffset end)
        {
            string typeName = HealthKitDataTypes.GetIdentifier(dataType);
            Debug.LogFormat("[{0}]: Reading quantity data for [{1}].", nameof(QuantityDataReader), typeName);
            bool operationCompleted = false;

            this._healthStore.ReadQuantitySamples(dataType, start, end, delegate (List<QuantitySample> samples, Error error)
            {
                if (error != null)
                {
                    HandleReadingError(new Exception($"Error reading {typeName} quantity data: {error.localizedDescription}"));
                    operationCompleted = true;
                    return;
                }

                if (samples.Count > 0)
                {
                    Debug.LogWarningFormat("[{0}]: {1} samples found for [{2}].", nameof(QuantityDataReader), samples.Count, typeName);
                    UserData.Instance.QuantityTypeValues[typeName] = samples;
                }
                else
                {
                    // In case there were not samples fill the dictionary with the value -
                    Debug.LogWarningFormat("[{0}]: No samples found for [{1}].", nameof(QuantityDataReader), typeName);
                }
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
