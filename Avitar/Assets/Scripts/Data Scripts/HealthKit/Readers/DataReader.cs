using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BeliefEngine.HealthKit
{
    // Base class for different types of data readers
    public abstract class DataReader : MonoBehaviour
    {
        protected HealthStore _healthStore;
        protected HealthKitDataTypes _healthKitDataTypes;

        // Coroutine method for reading data
        public abstract IEnumerator ReadDataAsync();

        protected void Awake()
        {
            _healthStore = GetComponent<HealthStore>();
            _healthKitDataTypes = GetComponent<HealthKitDataTypes>();
        }

        // Handle errors during reading operations
        protected void HandleReadingError(Exception e)
        {
            Debug.LogError("[Data Reader]: Reading error - " + e.Message);
            // You might want to perform additional error handling or logging here.
        }
    }



}
