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

    /// <summary>
    /// Callback function registered in the UnityEvent of InworldCharacter.
    /// </summary>
    /// <param name="trigger">the callback trigger to process.</param>
    public void OnGoalComplete(string trigger)
    {
        if (trigger == "initiate_hrv_analysis")
        {

            int heartRate = int.Parse(UserData.Instance.GetPlayerProfileFieldValue("heart_rate"));
            int highHeartRateEvents = int.Parse(UserData.Instance.GetPlayerProfileFieldValue("high_heart_rate_events"));
            int lowHeartRateEvents = int.Parse(UserData.Instance.GetPlayerProfileFieldValue("low_heart_rate_events"));
            int irregularHeartRateEvents = int.Parse(UserData.Instance.GetPlayerProfileFieldValue("irregular_heart_rhythm_events"));
            // Init the overall stress level
            float overallStressLevel = 0;

            if (heartRate < 30 || heartRate > 130)
            {
                overallStressLevel += 0.7f;
            }
            else if (heartRate < 40 || heartRate > 120)
            {
                overallStressLevel += 0.5f;
            }
            else if (heartRate < 50 || heartRate > 110)
            {
                overallStressLevel += 0.2f;
            }
            else
            {
                // Do nothing
            }


            overallStressLevel += 0.05f * Mathf.Min(highHeartRateEvents, 4);
            overallStressLevel += 0.05f * Mathf.Min(lowHeartRateEvents, 4);
            overallStressLevel += 0.05f * Mathf.Min(irregularHeartRateEvents, 4);


            Debug.Log("heartRate: " + heartRate);
            Debug.Log("highHeartRateEvents: " + highHeartRateEvents);
            Debug.Log("lowHeartRateEvents: " + lowHeartRateEvents);
            Debug.Log("irregularHeartRateEvents: " + irregularHeartRateEvents);

            Material[] temp = new Material[3];
            temp[0] = skinnedMeshRenderer.materials[0];
            temp[1] = skinnedMeshRenderer.materials[1];

            string label = "State of mind: ";
            // Analytical categorization based on overall stress level
            if (overallStressLevel >= 0.8f)
            {

                label += "Overwhelmed";
                temp[2] = stressLevelMaterials[4];
            }
            else if (overallStressLevel >= 0.6f)
            {
                label += "Highly Stressed";
                temp[2] = stressLevelMaterials[3];
            }
            else if (overallStressLevel >= 0.4f)
            {
                label += "Moderately Stressed";
                temp[2] = stressLevelMaterials[2];
            }
            else if (overallStressLevel >= 0.2f)
            {
                label += "Mildly Stressed";
                temp[2] = stressLevelMaterials[1];
            }
            else
            {
                label += "Not Stressed";
                temp[2] = stressLevelMaterials[0];
            }
            stressLabel.text = label;
            stressLabel.color = temp[2].color;
            skinnedMeshRenderer.materials = temp;
        }
    }
}
