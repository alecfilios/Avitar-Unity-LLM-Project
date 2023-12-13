using System.Collections;
using System.Collections.Generic;
using BeliefEngine.HealthKit;
using Unity.VisualScripting;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public static UserData Instance;

    public Dictionary<string, List<QuantitySample>> QuantityTypeValues { get; set; }
    public Dictionary<string, List<CategorySample>> CategoryTypeValues { get; set; }
    public Dictionary<string, Characteristic> CharacteristicTypeValues { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroying the GameObject the script is attached to
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This ensures the GameObject persists through scene changes
        }
    }

    public void InitializeDB()
    {
        InitQuantityData();
        InitCategoryData();
        InitCharacteristicData();
    }

    void InitQuantityData()
    {
        QuantityTypeValues = new Dictionary<string, List<QuantitySample>>();
        // ------------------------------------------------------------------------------------------------
        // [Body Measurments]
        // ------------------------------------------------------------------------------------------------
        // > Height: A quantity sample type that measures the user’s height.
        QuantityTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierHeight), null);
        // > Weight: A quantity sample type that measures the user’s weight.
        QuantityTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierBodyMass), null);
        // ------------------------------------------------------------------------------------------------
        // [Activity]
        // ------------------------------------------------------------------------------------------------
        // > Step Count: A quantity sample type that measures the number of steps the user has taken.
        QuantityTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierStepCount), null);
        // > Distance Walking Running: A quantity sample type that measures the distance the user has moved by walking or running.
        QuantityTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierDistanceWalkingRunning), null);
        // ------------------------------------------------------------------------------------------------
        // [Heart]
        // ------------------------------------------------------------------------------------------------
        // > Heart Rate: A quantity sample type that measures the user’s heart rate.
        QuantityTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierHeartRate), null);
        // ------------------------------------------------------------------------------------------------
        // [Diabetes]
        // ------------------------------------------------------------------------------------------------
        // > Blood Glucose: A quantity sample type that measures the user’s blood glucose level.
        QuantityTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierBloodGlucose), null);
        // > Insulin Delivery: A quantity sample that measures the amount of insulin delivered.
        QuantityTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierInsulinDelivery), null);
        // ------------------------------------------------------------------------------------------------
        // [Nutrition]
        // ------------------------------------------------------------------------------------------------
        // > Dietary Fat Total: A quantity sample type that measures the total amount of fat consumed.
        QuantityTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierDietaryFatTotal), null);
        // > Dietary Carbohydrates: A quantity sample type that measures the amount of carbohydrates consumed.
        QuantityTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierDietaryCarbohydrates), null);
        // > Dietary Protein: A quantity sample type that measures the amount of protein consumed.
        QuantityTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierDietaryProtein), null);
        // > Dietary Water: A quantity sample type that measures the amount of water consumed.
        QuantityTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierDietaryWater), null);
        // ------------------------------------------------------------------------------------------------
    }
    void InitCategoryData()
    {
        CategoryTypeValues = new Dictionary<string, List<CategorySample>>();
        // ------------------------------------------------------------------------------------------------
        // [Heart]
        // ------------------------------------------------------------------------------------------------
        // > High Heart Rate Event: A category sample type for high heart rate events.
        CategoryTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKCategoryTypeIdentifierHighHeartRateEvent), null);
        // > Low Heart Rate Event: A category sample type for low heart rate events.
        CategoryTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKCategoryTypeIdentifierLowHeartRateEvent), null);
        // > Irregular Heart Rhythm Event: A category sample type for irregular heart rhythm events.
        CategoryTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKCategoryTypeIdentifierIrregularHeartRhythmEvent), null);
    }

    void InitCharacteristicData()
    {
        CharacteristicTypeValues = new Dictionary<string, Characteristic>();
        // ------------------------------------------------------------------------------------------------
        // [Characteristics]
        // ------------------------------------------------------------------------------------------------
        // > Biological Sex: A characteristic type identifier for the user’s sex.
        CharacteristicTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKCharacteristicTypeIdentifierBiologicalSex), null);
        // > Blood Type: A characteristic type identifier for the user’s blood type.
        CharacteristicTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKCharacteristicTypeIdentifierBloodType), null);
        // > Date Of Birth: A characteristic type identifier for the user’s date of birth.
        CharacteristicTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKCharacteristicTypeIdentifierDateOfBirth), null);
        // > Fitzpatrick Skin Type: A characteristic type identifier for the user’s skin type.
        CharacteristicTypeValues.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKCharacteristicTypeIdentifierFitzpatrickSkinType), null);
    }
}
