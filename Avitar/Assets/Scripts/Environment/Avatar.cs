using System;
using System.Collections;
using System.Collections.Generic;
using BeliefEngine.HealthKit;
using TMPro;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    [SerializeField]
    TMP_Text stressLabel;

    [SerializeField]
    Animator animator;

    [SerializeField]
    ParticleSystem TeleportAuraParticleSystem;

    [SerializeField]
    SkinnedMeshRenderer skinnedMeshRenderer;

    [SerializeField]
    Material[] maskMaterials;
    [SerializeField]
    Material[] headAndBodyMaterials;
    [SerializeField]
    Material[] handsAndFeetMaterials;

    [SerializeField]
    Material[] stressLevelMaterials;

    public void Teleport(Island island)
    {
        transform.position = island.GetAvatarTarget_T().position;
        transform.rotation = island.GetAvatarTarget_T().transform.rotation;
        TeleportAuraParticleSystem.Play();
        ChangeAnimationState(island);
    }

    /*! @enum BiologicalSex
		@ingroup Enumerations
		@brief possible values for Biological Sex
	 */
    public enum BiologicalSex
    {
        NotSet = 0,
        Female,
        Male,
        Other
    }

    /*! @enum BloodType
		@ingroup Enumerations
		@brief possible values for BloodType
	 */
    public enum BloodType
    {
        NotSet = 0,
        APositive,
        ANegative,
        BPositive,
        BNegative,
        ABPositive,
        ABNegative,
        OPositive,
        ONegative
    }

    /*! @enum FitzpatrickSkinType
		@ingroup Enumerations
		@brief possible values for Fitzpatrick Skin Type
	 */
    public enum FitzpatrickSkinType
    {
        FitzpatrickSkinTypeNotSet = 0,
        FitzpatrickSkinTypeI,
        FitzpatrickSkinTypeII,
        FitzpatrickSkinTypeIII,
        FitzpatrickSkinTypeIV,
        FitzpatrickSkinTypeV,
        FitzpatrickSkinTypeVI
    }

    public void SetColors()
    {
        string sexString = UserData.Instance.CharacteristicTypeValues[HealthKitDataTypes.GetIdentifier(HKDataType.HKCharacteristicTypeIdentifierBiologicalSex)].ToString();
        string bloodTypeString = UserData.Instance.CharacteristicTypeValues[HealthKitDataTypes.GetIdentifier(HKDataType.HKCharacteristicTypeIdentifierBloodType)].ToString();
        string skinTypeString = UserData.Instance.CharacteristicTypeValues[HealthKitDataTypes.GetIdentifier(HKDataType.HKCharacteristicTypeIdentifierFitzpatrickSkinType)].ToString();

        Material[] temp = new Material[3];
        // Set color for Hands and Feet
        temp[0] = handsAndFeetMaterials[(int)Enum.Parse(typeof(BiologicalSex), sexString)];

        // Set color for Body and Head
        temp[1] = headAndBodyMaterials[(int)Enum.Parse(typeof(FitzpatrickSkinType), skinTypeString)];

        // Set color for Glass on the Mask
        temp[2] = maskMaterials[(int)Enum.Parse(typeof(BloodType), bloodTypeString)];

        skinnedMeshRenderer.materials = temp;

        // Inworld
        UserData.Instance.SetPlayerProfileFieldValue("blood_type", bloodTypeString);
        UserData.Instance.SetPlayerProfileFieldValue("fitzpatrick_skin_type", skinTypeString);
    }

    public void ChangeAnimationState(Island island)
    {
        AnimationClip clip = island.GetClip();
        animator.Play(clip.name, 0);
    }
    public string OnGoalComplete()
    {

        int heartRate = int.Parse(UserData.Instance.GetPlayerProfileFieldValue("heart_rate"));
        int highHeartRateEvents = int.Parse(UserData.Instance.GetPlayerProfileFieldValue("high_heart_rate_events"));
        int lowHeartRateEvents = int.Parse(UserData.Instance.GetPlayerProfileFieldValue("low_heart_rate_events"));
        int irregularHeartRateEvents = int.Parse(UserData.Instance.GetPlayerProfileFieldValue("irregular_heart_rhythm_events"));

        float baselineHRV = 50;
        float hrvScore = CalculateHRV(heartRate, baselineHRV);
        float overallStressLevel = 0;

        if (hrvScore < 20) overallStressLevel += 0.8f;
        else if (hrvScore < 30) overallStressLevel += 0.6f;
        else if (hrvScore < 40) overallStressLevel += 0.4f;
        else if (hrvScore < 50) overallStressLevel += 0.2f;

        overallStressLevel += 0.05f * Mathf.Min(highHeartRateEvents, 4);
        overallStressLevel += 0.05f * Mathf.Min(lowHeartRateEvents, 4);
        overallStressLevel += 0.05f * Mathf.Min(irregularHeartRateEvents, 4);

        Material[] temp = new Material[3];
        temp[0] = skinnedMeshRenderer.materials[0];
        temp[1] = skinnedMeshRenderer.materials[1];

        string label = overallStressLevel >= 0.8f ? "Overwhelmed" :
                       overallStressLevel >= 0.6f ? "Highly Stressed" :
                       overallStressLevel >= 0.4f ? "Moderately Stressed" :
                       overallStressLevel >= 0.2f ? "Mildly Stressed" : "Not Stressed";

        temp[2] = stressLevelMaterials[
            overallStressLevel >= 0.8f ? 4 :
            overallStressLevel >= 0.6f ? 3 :
            overallStressLevel >= 0.4f ? 2 :
            overallStressLevel >= 0.2f ? 1 : 0
        ];

        stressLabel.text = $"State of mind: {label}";
        stressLabel.color = temp[2].color;
        skinnedMeshRenderer.materials = temp;

        return $"HRV Score: {hrvScore}\nState of Mind: {label}";

    }

    private float CalculateHRV(int heartRate, float baselineHRV)
    {
        float hrv = baselineHRV - Mathf.Abs(heartRate - 60) * 0.5f;
        return Mathf.Max(hrv, 0);
    }

}
