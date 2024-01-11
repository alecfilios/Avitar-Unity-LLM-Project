using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BeliefEngine.HealthKit;
using System;

public class HeartPanel : OptionsPanelBase
{
    [SerializeField]
    ParticleSystem heartRateParticleSystem;

    [SerializeField]
    TMP_Text HeartRateText;

    [SerializeField]
    GameObject HighHeartRateEventPrefab;

    [SerializeField]
    GameObject LowHeartRateEventtPrefab;

    [SerializeField]
    GameObject IrregularHeartRhythmEventPrefab;

    [SerializeField]
    TMP_Text HighHeartRateEventNumberText;
    [SerializeField]
    TMP_Text IrregularHeartRhythmEventNumberText;
    [SerializeField]
    TMP_Text LowHeartRateEventNumberText;
    [SerializeField]
    Transform avatar_T;

    List<GameObject> events = new List<GameObject>();

    private void OnEnable()
    {

        heartRateParticleSystem.gameObject.SetActive(true);
        heartRateParticleSystem.Play();
        foreach (var item in events)
        {
            item.SetActive(true);
        }
    }

    private void OnDisable()
    {

        heartRateParticleSystem.Stop();
        heartRateParticleSystem.gameObject.SetActive(false);
        foreach (var item in events)
        {
            item.SetActive(false);
        }
    }
    public override void Init()
    {

        double heartRate = HealthDataUtils.GetMostRecentQuantityValue(HKDataType.HKQuantityTypeIdentifierHeartRate);
        int eventCounter = 0;
        HeartRateText.text = "Heart Rate: " + FormatQuantity(heartRate, "bpm", 0);
        // ----
        List<CategorySample> highHeartRateSamples = HealthDataUtils.GetCategorySamplesByType(HKDataType.HKCategoryTypeIdentifierHighHeartRateEvent);
        string highHeartRateEventText = "High Heart Rate Event:";
        foreach (var sample in highHeartRateSamples)
        {
            eventCounter += 1;
            highHeartRateEventText += $"({HealthDataUtils.FormatDate(sample.startDate)} - {HealthDataUtils.FormatDate(sample.endDate)})\n";
            CreateEventOrb(HighHeartRateEventPrefab);
        }
        HighHeartRateEventNumberText.text = eventCounter.ToString();
        eventCounter = 0;
        // ----
        List<CategorySample> lowHeartRateSamples = HealthDataUtils.GetCategorySamplesByType(HKDataType.HKCategoryTypeIdentifierLowHeartRateEvent);
        string lowHeartRateEventText = "Low Heart Rate Event: ";
        foreach (var sample in lowHeartRateSamples)
        {
            eventCounter += 1;
            lowHeartRateEventText += $"({HealthDataUtils.FormatDate(sample.startDate)} - {HealthDataUtils.FormatDate(sample.endDate)})\n";
            CreateEventOrb(LowHeartRateEventtPrefab);
        }
        LowHeartRateEventNumberText.text = eventCounter.ToString();
        eventCounter = 0;
        // ----
        List<CategorySample> irregularHeartRhythmSamples = HealthDataUtils.GetCategorySamplesByType(HKDataType.HKCategoryTypeIdentifierIrregularHeartRhythmEvent);
        string irregularHeartRhythmEventText = "Irregular Heart Rhythm Event: ";
        foreach (var sample in irregularHeartRhythmSamples)
        {
            eventCounter += 1;
            irregularHeartRhythmEventText += $"({HealthDataUtils.FormatDate(sample.startDate)} - {HealthDataUtils.FormatDate(sample.endDate)})\n";
            CreateEventOrb(IrregularHeartRhythmEventPrefab);
        }
        IrregularHeartRhythmEventNumberText.text = eventCounter.ToString();
        eventCounter = 0;

    }

    private string FormatQuantity(double value, string unit, int digits)
    {
        // Round the value to 0 decimal places and append the unit
        return $"{Math.Round(value, digits)} {unit}";
    }

    private GameObject CreateEventOrb(GameObject prefab)
    {

        GameObject go = Instantiate(prefab, avatar_T);
        events.Add(go);
        go.SetActive(false);
        return go;
    }

}

