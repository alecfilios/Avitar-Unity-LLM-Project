using System.Collections;
using UnityEngine;
using BeliefEngine.HealthKit;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    HealthKitManager _healthKitManagerManager;

    [SerializeField]
    UIManager _uIManager;

    private void Awake()
    {
        UserData.Instance.InitializeDB();
    }

    public void CallLoadDataFromHealthKitAsync()
    {
        StartCoroutine(LoadDataFromHealthKitAsync());
    }

    IEnumerator LoadDataFromHealthKitAsync()
    {
        _healthKitManagerManager.ReadData();

        // Wait for the data to be ready
        while (!_healthKitManagerManager.IsReady)
        {
            yield return null;
        }

        _uIManager.InitializeTexts();
    }
}
