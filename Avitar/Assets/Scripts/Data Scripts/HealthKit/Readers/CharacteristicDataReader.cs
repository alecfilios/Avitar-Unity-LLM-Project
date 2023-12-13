using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

namespace BeliefEngine.HealthKit
{
    public class CharacteristicDataReader : DataReader
    {
        public override IEnumerator ReadDataAsync()
        {
            var characteristicKeyList = new List<string>(UserData.Instance.CharacteristicTypeValues.Keys);

            foreach (var key in characteristicKeyList)
            {
                HKDataType dataType = (HKDataType)Enum.Parse(typeof(HKDataType), key);
                yield return StartCoroutine(ReadValue(dataType));
            }
        }

        private IEnumerator ReadValue(HKDataType dataType)
        {
            string typeName = HealthKitDataTypes.GetIdentifier(dataType);
            Debug.LogFormat("[{0}]: Reading characteristic data for [{1}].", nameof(CharacteristicDataReader), typeName);
            bool operationCompleted = false;
            this._healthStore.ReadCharacteristic(dataType, delegate (Characteristic characteristic, Error error)
            {
                if (error != null)
                {
                    HandleReadingError(new Exception($"Error reading {typeName} characteristic data: {error.localizedDescription}"));
                    operationCompleted = true;
                    return;
                }

                UserData.Instance.CharacteristicTypeValues[typeName] = characteristic;
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
