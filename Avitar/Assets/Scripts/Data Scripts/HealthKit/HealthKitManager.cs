using UnityEngine;
using System;
using System.Collections.Generic;
using BeliefEngine.HealthKit;
using System.Collections;

namespace BeliefEngine.HealthKit
{
    public class HealthKitManager : MonoBehaviour
    {
        #region Private
        private HealthStore _healthStore;
        private HealthKitDataTypes _healthKitDataTypes;
        private QuantityDataReader _quantityDataReader;
        private CategoryDataReader _categoryDataReader;
        private CharacteristicDataReader _characteristicDataReader;

        private bool _isReady = false; // Indicates whether the health data is ready
        #endregion

        public bool IsReady
        {
            get { return _isReady; }
        }

        public void Awake()
        {
            // Initialize health store and data types
            _healthStore = GetComponent<HealthStore>();
            _healthKitDataTypes = GetComponent<HealthKitDataTypes>();
            // Initialize the data readers
            _quantityDataReader = GetComponent<QuantityDataReader>();
            _categoryDataReader = GetComponent<CategoryDataReader>();
            _characteristicDataReader = GetComponent<CharacteristicDataReader>();

            // Check if the platform is iOS or if running in the editor
            if (Application.platform != RuntimePlatform.IPhonePlayer && Application.isEditor)
            {
                Debug.LogWarning("[HealthKit Manager]: HealthKit only works on iOS devices!");
            }
            else
            {
                // Authorize access to health data
                _healthStore.Authorize(_healthKitDataTypes);
            }
        }

        public void ReadData()
        {
            // If running in the editor, use fake data from FakeDB
            if (Application.platform != RuntimePlatform.IPhonePlayer && Application.isEditor)
            {
                FakeDB.GenerateFakeData(); // Generate fake data
                _isReady = true; // Set _isReady to true when all reads are completed
            }
            else
            {
                try
                {
                    StartCoroutine(ReadDataAsync());
                }
                catch (Exception e)
                {
                    HandleReadingError(e);
                }
            }

        }

        private IEnumerator ReadDataAsync()
        {

            // List of data readers
            var dataReaders = new List<DataReader> { _quantityDataReader, _categoryDataReader, _characteristicDataReader };

            // Iterate through data readers and read data
            foreach (var dataReader in dataReaders)
            {
                yield return StartCoroutine(dataReader.ReadDataAsync());
            }

            _isReady = true; // Set _isReady to true when all reads are completed
        }

        // Handle errors during reading operations
        private void HandleReadingError(Exception e)
        {
            Debug.LogError("[HealthKit Manager]: Reading error - " + e.Message);
            // You might want to perform additional error handling or logging here.
        }
    }
}
