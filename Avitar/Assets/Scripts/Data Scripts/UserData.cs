using System.Collections;
using System.Collections.Generic;
using BeliefEngine.HealthKit;
using Inworld;
using Unity.VisualScripting;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public static UserData Instance;

    [SerializeField]
    InworldUserSetting InWorldUserSettings;

    public Dictionary<string, List<QuantitySample>> QuantityTypeValues { get; set; }
    public Dictionary<string, List<CategorySample>> CategoryTypeValues { get; set; }
    public Dictionary<string, Characteristic> CharacteristicTypeValues { get; set; }

    Dictionary<string, string> HealthData { get; set; }
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
        HealthData = new Dictionary<string, string>();
    }

    void InitQuantityData()
    {
        QuantityTypeValues = new Dictionary<string, List<QuantitySample>>
        {
            // ------------------------------------------------------------------------------------------------
            // [Body Measurments]
            // ------------------------------------------------------------------------------------------------
            // > Height: A quantity sample type that measures the user’s height.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierHeight), null },
            // > Weight: A quantity sample type that measures the user’s weight.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierBodyMass), null },
            // ------------------------------------------------------------------------------------------------
            // [Activity]
            // ------------------------------------------------------------------------------------------------
            // > Step Count: A quantity sample type that measures the number of steps the user has taken.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierStepCount), null },
            // > Distance Walking Running: A quantity sample type that measures the distance the user has moved by walking or running.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierDistanceWalkingRunning), null },
            // ------------------------------------------------------------------------------------------------
            // [Heart]
            // ------------------------------------------------------------------------------------------------
            // > Heart Rate: A quantity sample type that measures the user’s heart rate.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierHeartRate), null },
            // ------------------------------------------------------------------------------------------------
            // [Diabetes]
            // ------------------------------------------------------------------------------------------------
            // > Blood Glucose: A quantity sample type that measures the user’s blood glucose level.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierBloodGlucose), null },
            // > Insulin Delivery: A quantity sample that measures the amount of insulin delivered.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierInsulinDelivery), null },
            // ------------------------------------------------------------------------------------------------
            // [Nutrition]
            // ------------------------------------------------------------------------------------------------
            // > Dietary Fat Total: A quantity sample type that measures the total amount of fat consumed.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierDietaryFatTotal), null },
            // > Dietary Carbohydrates: A quantity sample type that measures the amount of carbohydrates consumed.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierDietaryCarbohydrates), null },
            // > Dietary Protein: A quantity sample type that measures the amount of protein consumed.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierDietaryProtein), null },
            // > Dietary Water: A quantity sample type that measures the amount of water consumed.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKQuantityTypeIdentifierDietaryWater), null }
        };
        // ------------------------------------------------------------------------------------------------
    }
    void InitCategoryData()
    {
        CategoryTypeValues = new Dictionary<string, List<CategorySample>>
        {
            // ------------------------------------------------------------------------------------------------
            // [Heart]
            // ------------------------------------------------------------------------------------------------
            // > High Heart Rate Event: A category sample type for high heart rate events.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKCategoryTypeIdentifierHighHeartRateEvent), null },
            // > Low Heart Rate Event: A category sample type for low heart rate events.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKCategoryTypeIdentifierLowHeartRateEvent), null },
            // > Irregular Heart Rhythm Event: A category sample type for irregular heart rhythm events.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKCategoryTypeIdentifierIrregularHeartRhythmEvent), null }
        };
    }

    void InitCharacteristicData()
    {
        CharacteristicTypeValues = new Dictionary<string, Characteristic>
        {
            // ------------------------------------------------------------------------------------------------
            // [Characteristics]
            // ------------------------------------------------------------------------------------------------
            // > Biological Sex: A characteristic type identifier for the user’s sex.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKCharacteristicTypeIdentifierBiologicalSex), null },
            // > Blood Type: A characteristic type identifier for the user’s blood type.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKCharacteristicTypeIdentifierBloodType), null },
            // > Date Of Birth: A characteristic type identifier for the user’s date of birth.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKCharacteristicTypeIdentifierDateOfBirth), null },
            // > Fitzpatrick Skin Type: A characteristic type identifier for the user’s skin type.
            { HealthKitDataTypes.GetIdentifier(HKDataType.HKCharacteristicTypeIdentifierFitzpatrickSkinType), null }
        };
    }

    public void SetPlayerProfileFieldValue(string id, string value)
    {

        // InWorldUserSettings.SetPlayerProfileFieldValue(id, value);
        HealthData.Add(id, value);
    }

    public string GetPlayerProfileFieldValue(string id)
    {
        return HealthData[id];
    }

    public string GetHealthDictionaryFormatted()
    {
        string healthData = "\n Strictly keep your answer brief\n[Optional]Health Data of the user for the last 24 hours [User them in your answer if needed]:\n";
        foreach (var entry in HealthData)
        {
            healthData += $"{entry.Key}: {entry.Value}\n";
        }
        return healthData;
    }
}
